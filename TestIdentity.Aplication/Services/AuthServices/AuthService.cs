using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TestIdentity.Aplication.DTOs.AuthDTOs;
using TestIdentity.Domain.Entities;
using TestIdentity.Domain.Utilities.Concretes;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.Aplication.Services.AuthServices
{
    /// <summary>
    /// Authentication Service - Kimlik doğrulama servisi implementasyonu
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Kullanıcı kaydı yapar / Registers a new user
        /// </summary>
        public async Task<IResult> RegisterAsync(RegisterDTO registerDTO)
        {
            // Kullanıcı adı kontrolü / Check username
            var existingUser = await _userManager.FindByNameAsync(registerDTO.UserName);
            if (existingUser != null)
            {
                return new ErrorResult("Bu kullanıcı adı zaten kullanılıyor");
            }

            // E-posta kontrolü / Check email
            existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                return new ErrorResult("Bu e-posta adresi zaten kullanılıyor");
            }

            // Yeni kullanıcı oluştur / Create new user
            var user =registerDTO.Adapt<ApplicationUser>();

            //var user = new ApplicationUser
            //{
            //    UserName = registerDTO.UserName,
            //    Email = registerDTO.Email,
            //    FirstName = registerDTO.FirstName,
            //    LastName = registerDTO.LastName,
            //    PhoneNumber = registerDTO.PhoneNumber,
            //    EmailConfirmed = false,
            //    CreatedDate = DateTime.UtcNow,
            //    IsActive = true
            //};

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Kullanıcı oluşturulamadı: {errors}");
            }

            // Varsayılan rol atama / Assign default role
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = "User", CreatedDate = DateTime.UtcNow });
            }
            await _userManager.AddToRoleAsync(user, "User");

            return new SuccessResult("Kullanıcı başarıyla kaydedildi. E-posta onayı için lütfen e-postanızı kontrol edin.");
        }

        /// <summary>
        /// Kullanıcı girişi yapar ve token döner / Logs in user and returns token
        /// </summary>
        public async Task<IDataResult<TokenDTO>> LoginAsync(LoginDTO loginDTO)
        {
            // Kullanıcıyı bul / Find user
            var user = await _userManager.FindByNameAsync(loginDTO.UserNameOrEmail) 
                ?? await _userManager.FindByEmailAsync(loginDTO.UserNameOrEmail);

            if (user == null)
            {
                return new ErrorDataResult<TokenDTO>(null, "Kullanıcı adı veya şifre hatalı");
            }

            if (!user.IsActive)
            {
                return new ErrorDataResult<TokenDTO>(null, "Hesabınız devre dışı bırakılmış");
            }

            // Şifre kontrolü / Check password
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: true);
            
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    return new ErrorDataResult<TokenDTO>(null, "Hesabınız geçici olarak kilitlenmiştir. Lütfen daha sonra tekrar deneyin.");
                }
                return new ErrorDataResult<TokenDTO>(null, "Kullanıcı adı veya şifre hatalı");
            }

            // İki faktörlü kimlik doğrulama kontrolü / Check two factor authentication
            if (user.TwoFactorEnabled)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                return new ErrorDataResult<TokenDTO>(null, "İki faktörlü kimlik doğrulama gereklidir");
            }

            // Token oluştur / Generate token
            var token = await GenerateTokenAsync(user);
            return new SuccessDataResult<TokenDTO>(token, "Giriş başarılı");
        }

        /// <summary>
        /// Refresh token ile yeni access token alır / Gets new access token with refresh token
        /// </summary>
        public async Task<IDataResult<TokenDTO>> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenDTO.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new ErrorDataResult<TokenDTO>(null, "Geçersiz veya süresi dolmuş refresh token");
            }

            var token = await GenerateTokenAsync(user);
            return new SuccessDataResult<TokenDTO>(token, "Token yenilendi");
        }

        /// <summary>
        /// Kullanıcı çıkışı yapar / Logs out user
        /// </summary>
        public async Task<IResult> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);

            return new SuccessResult("Çıkış başarılı");
        }

        /// <summary>
        /// E-posta onayı gönderir / Sends email confirmation
        /// </summary>
        public async Task<IResult> SendEmailConfirmationAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            if (user.EmailConfirmed)
            {
                return new ErrorResult("E-posta zaten onaylanmış");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // Burada e-posta gönderme servisi çağrılabilir / Email service can be called here
            // await _emailService.SendEmailConfirmationAsync(user.Email, token);

            return new SuccessResult("E-posta onay linki gönderildi");
        }

        /// <summary>
        /// E-posta onayını doğrular / Confirms email
        /// </summary>
        public async Task<IResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return new ErrorResult("E-posta onayı başarısız");
            }

            return new SuccessResult("E-posta başarıyla onaylandı");
        }

        /// <summary>
        /// Şifre unutma e-postası gönderir / Sends forgot password email
        /// </summary>
        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user == null)
            {
                // Güvenlik nedeniyle kullanıcı bulunamadı mesajı vermiyoruz / For security reasons, we don't reveal if user exists
                return new SuccessResult("Eğer bu e-posta adresi kayıtlıysa, şifre sıfırlama linki gönderildi");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Burada e-posta gönderme servisi çağrılabilir / Email service can be called here
            // await _emailService.SendPasswordResetAsync(user.Email, token);

            return new SuccessResult("Eğer bu e-posta adresi kayıtlıysa, şifre sıfırlama linki gönderildi");
        }

        /// <summary>
        /// Şifre sıfırlar / Resets password
        /// </summary>
        public async Task<IResult> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Şifre sıfırlama başarısız: {errors}");
            }

            return new SuccessResult("Şifre başarıyla sıfırlandı");
        }

        /// <summary>
        /// Şifre değiştirir / Changes password
        /// </summary>
        public async Task<IResult> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Şifre değiştirme başarısız: {errors}");
            }

            return new SuccessResult("Şifre başarıyla değiştirildi");
        }

        /// <summary>
        /// İki faktörlü kimlik doğrulama kodunu gönderir / Sends two factor authentication code
        /// </summary>
        public async Task<IResult> SendTwoFactorCodeAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            if (!user.TwoFactorEnabled)
            {
                return new ErrorResult("İki faktörlü kimlik doğrulama etkin değil");
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            // Burada e-posta gönderme servisi çağrılabilir / Email service can be called here
            // await _emailService.SendTwoFactorCodeAsync(user.Email, token);

            return new SuccessResult("İki faktörlü kimlik doğrulama kodu gönderildi");
        }

        /// <summary>
        /// İki faktörlü kimlik doğrulamayı doğrular / Verifies two factor authentication
        /// </summary>
        public async Task<IDataResult<TokenDTO>> VerifyTwoFactorAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorDataResult<TokenDTO>(null, "Kullanıcı bulunamadı");
            }

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", code);
            if (!result)
            {
                return new ErrorDataResult<TokenDTO>(null, "Geçersiz kod");
            }

            var token = await GenerateTokenAsync(user);
            return new SuccessDataResult<TokenDTO>(token, "İki faktörlü kimlik doğrulama başarılı");
        }

        /// <summary>
        /// JWT token oluşturur / Generates JWT token
        /// </summary>
        private async Task<TokenDTO> GenerateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Rolleri ekle / Add roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"] ?? "60"));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new TokenDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresAt = expires,
                TokenType = "Bearer"
            };
        }

        /// <summary>
        /// Refresh token oluşturur / Generates refresh token
        /// </summary>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}


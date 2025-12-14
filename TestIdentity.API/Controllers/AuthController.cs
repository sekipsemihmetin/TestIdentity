using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestIdentity.Aplication.DTOs.AuthDTOs;
using TestIdentity.Aplication.Services.AuthServices;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.API.Controllers
{
    /// <summary>
    /// Authentication Controller - Kimlik doğrulama işlemleri / Authentication operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Kullanıcı kaydı yapar / Registers a new user
        /// </summary>
        /// <param name="registerDTO">Kayıt bilgileri / Registration information</param>
        /// <returns>Kayıt sonucu / Registration result</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(registerDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcı girişi yapar / Logs in user
        /// </summary>
        /// <param name="loginDTO">Giriş bilgileri / Login information</param>
        /// <returns>Token bilgileri / Token information</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }

        /// <summary>
        /// Refresh token ile yeni access token alır / Gets new access token with refresh token
        /// </summary>
        /// <param name="refreshTokenDTO">Refresh token bilgisi / Refresh token information</param>
        /// <returns>Yeni token bilgileri / New token information</returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RefreshTokenAsync(refreshTokenDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }

        /// <summary>
        /// Kullanıcı çıkışı yapar / Logs out user
        /// </summary>
        /// <returns>Çıkış sonucu / Logout result</returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı bilgisi bulunamadı");
            }

            var result = await _authService.LogoutAsync(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// E-posta onayı gönderir / Sends email confirmation
        /// </summary>
        /// <param name="email">E-posta adresi / Email address</param>
        /// <returns>Gönderim sonucu / Send result</returns>
        [HttpPost("send-email-confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmailConfirmation([FromQuery] string email)
        {
            var result = await _authService.SendEmailConfirmationAsync(email);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// E-posta onayını doğrular / Confirms email
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si / User ID</param>
        /// <param name="token">Onay token'ı / Confirmation token</param>
        /// <returns>Onay sonucu / Confirmation result</returns>
        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _authService.ConfirmEmailAsync(userId, token);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Şifre unutma e-postası gönderir / Sends forgot password email
        /// </summary>
        /// <param name="forgotPasswordDTO">E-posta bilgisi / Email information</param>
        /// <returns>Gönderim sonucu / Send result</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.ForgotPasswordAsync(forgotPasswordDTO);
            return Ok(result);
        }

        /// <summary>
        /// Şifre sıfırlar / Resets password
        /// </summary>
        /// <param name="resetPasswordDTO">Şifre sıfırlama bilgileri / Password reset information</param>
        /// <returns>Sıfırlama sonucu / Reset result</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.ResetPasswordAsync(resetPasswordDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Şifre değiştirir / Changes password
        /// </summary>
        /// <param name="changePasswordDTO">Şifre değiştirme bilgileri / Password change information</param>
        /// <returns>Değiştirme sonucu / Change result</returns>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı bilgisi bulunamadı");
            }

            var result = await _authService.ChangePasswordAsync(userId, changePasswordDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// İki faktörlü kimlik doğrulama kodunu gönderir / Sends two factor authentication code
        /// </summary>
        /// <returns>Gönderim sonucu / Send result</returns>
        [HttpPost("send-two-factor-code")]
        [Authorize]
        public async Task<IActionResult> SendTwoFactorCode()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı bilgisi bulunamadı");
            }

            var result = await _authService.SendTwoFactorCodeAsync(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// İki faktörlü kimlik doğrulamayı doğrular / Verifies two factor authentication
        /// </summary>
        /// <param name="code">Doğrulama kodu / Verification code</param>
        /// <returns>Token bilgileri / Token information</returns>
        [HttpPost("verify-two-factor")]
        [Authorize]
        public async Task<IActionResult> VerifyTwoFactor([FromQuery] string code)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı bilgisi bulunamadı");
            }

            var result = await _authService.VerifyTwoFactorAsync(userId, code);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}


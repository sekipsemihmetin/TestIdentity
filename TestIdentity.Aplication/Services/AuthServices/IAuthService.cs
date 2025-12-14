using TestIdentity.Aplication.DTOs.AuthDTOs;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.Aplication.Services.AuthServices
{
    /// <summary>
    /// Authentication Service Interface - Kimlik doğrulama servisi arayüzü
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Kullanıcı kaydı yapar / Registers a new user
        /// </summary>
        Task<IResult> RegisterAsync(RegisterDTO registerDTO);

        /// <summary>
        /// Kullanıcı girişi yapar ve token döner / Logs in user and returns token
        /// </summary>
        Task<IDataResult<TokenDTO>> LoginAsync(LoginDTO loginDTO);

        /// <summary>
        /// Refresh token ile yeni access token alır / Gets new access token with refresh token
        /// </summary>
        Task<IDataResult<TokenDTO>> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);

        /// <summary>
        /// Kullanıcı çıkışı yapar / Logs out user
        /// </summary>
        Task<IResult> LogoutAsync(string userId);

        /// <summary>
        /// E-posta onayı gönderir / Sends email confirmation
        /// </summary>
        Task<IResult> SendEmailConfirmationAsync(string email);

        /// <summary>
        /// E-posta onayını doğrular / Confirms email
        /// </summary>
        Task<IResult> ConfirmEmailAsync(string userId, string token);

        /// <summary>
        /// Şifre unutma e-postası gönderir / Sends forgot password email
        /// </summary>
        Task<IResult> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);

        /// <summary>
        /// Şifre sıfırlar / Resets password
        /// </summary>
        Task<IResult> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);

        /// <summary>
        /// Şifre değiştirir / Changes password
        /// </summary>
        Task<IResult> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDTO);

        /// <summary>
        /// İki faktörlü kimlik doğrulama kodunu gönderir / Sends two factor authentication code
        /// </summary>
        Task<IResult> SendTwoFactorCodeAsync(string userId);

        /// <summary>
        /// İki faktörlü kimlik doğrulamayı doğrular / Verifies two factor authentication
        /// </summary>
        Task<IDataResult<TokenDTO>> VerifyTwoFactorAsync(string userId, string code);
    }
}


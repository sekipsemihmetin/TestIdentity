using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.AuthDTOs
{
    /// <summary>
    /// Şifre sıfırlama DTO'su / Reset password DTO
    /// </summary>
    public class ResetPasswordDTO
    {
        /// <summary>
        /// E-posta adresi / Email address
        /// </summary>
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Token / Şifre sıfırlama token'ı
        /// </summary>
        [Required(ErrorMessage = "Token gereklidir")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Yeni şifre / New password
        /// </summary>
        [Required(ErrorMessage = "Yeni şifre gereklidir")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Yeni şifre tekrarı / New password confirmation
        /// </summary>
        [Required(ErrorMessage = "Şifre tekrarı gereklidir")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}


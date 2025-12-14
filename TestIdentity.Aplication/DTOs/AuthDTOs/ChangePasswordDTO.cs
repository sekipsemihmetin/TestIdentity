using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.AuthDTOs
{
    /// <summary>
    /// Şifre değiştirme DTO'su / Change password DTO
    /// </summary>
    public class ChangePasswordDTO
    {
        /// <summary>
        /// Mevcut şifre / Current password
        /// </summary>
        [Required(ErrorMessage = "Mevcut şifre gereklidir")]
        public string CurrentPassword { get; set; } = string.Empty;

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


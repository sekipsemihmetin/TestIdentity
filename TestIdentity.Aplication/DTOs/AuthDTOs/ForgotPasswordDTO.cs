using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.AuthDTOs
{
    /// <summary>
    /// Şifre unutma DTO'su / Forgot password DTO
    /// </summary>
    public class ForgotPasswordDTO
    {
        /// <summary>
        /// E-posta adresi / Email address
        /// </summary>
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;
    }
}


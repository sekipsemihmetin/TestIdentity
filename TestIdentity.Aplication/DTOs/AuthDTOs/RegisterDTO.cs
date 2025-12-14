using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.AuthDTOs
{
    /// <summary>
    /// Kullanıcı kayıt DTO'su / User registration DTO
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Kullanıcı adı / Username
        /// </summary>
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// E-posta adresi / Email address
        /// </summary>
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Şifre / Password
        /// </summary>
        [Required(ErrorMessage = "Şifre gereklidir")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Şifre tekrarı / Password confirmation
        /// </summary>
        [Required(ErrorMessage = "Şifre tekrarı gereklidir")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Ad / First name
        /// </summary>
        [Required(ErrorMessage = "Ad gereklidir")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Soyad / Last name
        /// </summary>
        [Required(ErrorMessage = "Soyad gereklidir")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Telefon numarası / Phone number
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}


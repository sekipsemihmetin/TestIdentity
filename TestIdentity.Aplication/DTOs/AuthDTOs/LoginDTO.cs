using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.AuthDTOs
{
    /// <summary>
    /// Kullanıcı giriş DTO'su / User login DTO
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Kullanıcı adı veya e-posta / Username or email
        /// </summary>
        [Required(ErrorMessage = "Kullanıcı adı veya e-posta gereklidir")]
        public string UserNameOrEmail { get; set; } = string.Empty;

        /// <summary>
        /// Şifre / Password
        /// </summary>
        [Required(ErrorMessage = "Şifre gereklidir")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Beni hatırla / Remember me
        /// </summary>
        public bool RememberMe { get; set; } = false;
    }
}


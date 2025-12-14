using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.AuthDTOs
{
    /// <summary>
    /// Refresh token DTO'su / Refresh token DTO
    /// </summary>
    public class RefreshTokenDTO
    {
        /// <summary>
        /// Refresh token / Yenileme token'ı
        /// </summary>
        [Required(ErrorMessage = "Refresh token gereklidir")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}


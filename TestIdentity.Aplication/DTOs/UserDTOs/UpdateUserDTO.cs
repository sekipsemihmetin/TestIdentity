using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.UserDTOs
{
    /// <summary>
    /// Kullanıcı güncelleme DTO'su / Update user DTO
    /// </summary>
    public class UpdateUserDTO
    {
        /// <summary>
        /// Kullanıcı ID'si / User ID
        /// </summary>
        [Required]
        public string Id { get; set; } = string.Empty;

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


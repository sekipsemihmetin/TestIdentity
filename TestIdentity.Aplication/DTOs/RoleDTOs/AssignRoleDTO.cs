using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.RoleDTOs
{
    /// <summary>
    /// Rol atama DTO'su / Assign role DTO
    /// </summary>
    public class AssignRoleDTO
    {
        /// <summary>
        /// Kullanıcı ID'si / User ID
        /// </summary>
        [Required(ErrorMessage = "Kullanıcı ID'si gereklidir")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Rol adı / Role name
        /// </summary>
        [Required(ErrorMessage = "Rol adı gereklidir")]
        public string RoleName { get; set; } = string.Empty;
    }
}


using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Aplication.DTOs.RoleDTOs
{
    /// <summary>
    /// Rol oluşturma DTO'su / Create role DTO
    /// </summary>
    public class CreateRoleDTO
    {
        /// <summary>
        /// Rol adı / Role name
        /// </summary>
        [Required(ErrorMessage = "Rol adı gereklidir")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Rol açıklaması / Role description
        /// </summary>
        public string? Description { get; set; }
    }
}


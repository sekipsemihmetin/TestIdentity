namespace TestIdentity.Aplication.DTOs.RoleDTOs
{
    /// <summary>
    /// Rol DTO'su / Role DTO
    /// </summary>
    public class RoleDTO
    {
        /// <summary>
        /// Rol ID'si / Role ID
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Rol adı / Role name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Rol açıklaması / Role description
        /// </summary>
        public string? Description { get; set; }
    }
}


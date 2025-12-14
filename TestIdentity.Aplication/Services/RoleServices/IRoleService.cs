using TestIdentity.Aplication.DTOs.RoleDTOs;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.Aplication.Services.RoleServices
{
    /// <summary>
    /// Role Service Interface - Rol servisi arayüzü
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Tüm rolleri getirir / Gets all roles
        /// </summary>
        Task<IDataResult<List<RoleDTO>>> GetAllRolesAsync();

        /// <summary>
        /// ID'ye göre rol getirir / Gets role by ID
        /// </summary>
        Task<IDataResult<RoleDTO>> GetRoleByIdAsync(string id);

        /// <summary>
        /// Yeni rol oluşturur / Creates new role
        /// </summary>
        Task<IResult> CreateRoleAsync(CreateRoleDTO createRoleDTO);

        /// <summary>
        /// Rolü günceller / Updates role
        /// </summary>
        Task<IResult> UpdateRoleAsync(string id, CreateRoleDTO updateRoleDTO);

        /// <summary>
        /// Rolü siler / Deletes role
        /// </summary>
        Task<IResult> DeleteRoleAsync(string id);

        /// <summary>
        /// Rolün kullanıcılarını getirir / Gets role users
        /// </summary>
        Task<IDataResult<List<string>>> GetRoleUsersAsync(string roleId);
    }
}


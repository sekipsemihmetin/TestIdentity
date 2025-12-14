using TestIdentity.Aplication.DTOs.UserDTOs;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.Aplication.Services.UserServices
{
    /// <summary>
    /// User Service Interface - Kullanıcı servisi arayüzü
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Tüm kullanıcıları getirir / Gets all users
        /// </summary>
        Task<IDataResult<List<UserDTO>>> GetAllUsersAsync();

        /// <summary>
        /// ID'ye göre kullanıcı getirir / Gets user by ID
        /// </summary>
        Task<IDataResult<UserDTO>> GetUserByIdAsync(string id);

        /// <summary>
        /// Kullanıcı günceller / Updates user
        /// </summary>
        Task<IResult> UpdateUserAsync(UpdateUserDTO updateUserDTO);

        /// <summary>
        /// Kullanıcıyı siler (soft delete) / Deletes user (soft delete)
        /// </summary>
        Task<IResult> DeleteUserAsync(string id);

        /// <summary>
        /// Kullanıcıyı aktif/pasif yapar / Activates/deactivates user
        /// </summary>
        Task<IResult> ToggleUserStatusAsync(string id);

        /// <summary>
        /// Kullanıcının rollerini getirir / Gets user roles
        /// </summary>
        Task<IDataResult<List<string>>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Kullanıcıya rol atar / Assigns role to user
        /// </summary>
        Task<IResult> AssignRoleToUserAsync(string userId, string roleName);

        /// <summary>
        /// Kullanıcıdan rol kaldırır / Removes role from user
        /// </summary>
        Task<IResult> RemoveRoleFromUserAsync(string userId, string roleName);
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestIdentity.Aplication.DTOs.UserDTOs;
using TestIdentity.Aplication.Services.UserServices;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.API.Controllers
{
    /// <summary>
    /// User Controller - Kullanıcı yönetimi işlemleri / User management operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Tüm kullanıcıları getirir / Gets all users
        /// </summary>
        /// <returns>Kullanıcı listesi / User list</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// ID'ye göre kullanıcı getirir / Gets user by ID
        /// </summary>
        /// <param name="id">Kullanıcı ID'si / User ID</param>
        /// <returns>Kullanıcı bilgisi / User information</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Kullanıcı günceller / Updates user
        /// </summary>
        /// <param name="updateUserDTO">Güncelleme bilgileri / Update information</param>
        /// <returns>Güncelleme sonucu / Update result</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUserAsync(updateUserDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcıyı siler / Deletes user
        /// </summary>
        /// <param name="id">Kullanıcı ID'si / User ID</param>
        /// <returns>Silme sonucu / Delete result</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcıyı aktif/pasif yapar / Activates/deactivates user
        /// </summary>
        /// <param name="id">Kullanıcı ID'si / User ID</param>
        /// <returns>Durum değiştirme sonucu / Status change result</returns>
        [HttpPost("{id}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleUserStatus(string id)
        {
            var result = await _userService.ToggleUserStatusAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcının rollerini getirir / Gets user roles
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si / User ID</param>
        /// <returns>Rol listesi / Role list</returns>
        [HttpGet("{userId}/roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var result = await _userService.GetUserRolesAsync(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcıya rol atar / Assigns role to user
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si / User ID</param>
        /// <param name="roleName">Rol adı / Role name</param>
        /// <returns>Atama sonucu / Assignment result</returns>
        [HttpPost("{userId}/roles/{roleName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var result = await _userService.AssignRoleToUserAsync(userId, roleName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcıdan rol kaldırır / Removes role from user
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si / User ID</param>
        /// <param name="roleName">Rol adı / Role name</param>
        /// <returns>Kaldırma sonucu / Removal result</returns>
        [HttpDelete("{userId}/roles/{roleName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var result = await _userService.RemoveRoleFromUserAsync(userId, roleName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}


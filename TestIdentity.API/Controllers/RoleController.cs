using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestIdentity.Aplication.DTOs.RoleDTOs;
using TestIdentity.Aplication.Services.RoleServices;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.API.Controllers
{
    /// <summary>
    /// Role Controller - Rol yönetimi işlemleri / Role management operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Tüm rolleri getirir / Gets all roles
        /// </summary>
        /// <returns>Rol listesi / Role list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// ID'ye göre rol getirir / Gets role by ID
        /// </summary>
        /// <param name="id">Rol ID'si / Role ID</param>
        /// <returns>Rol bilgisi / Role information</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Yeni rol oluşturur / Creates new role
        /// </summary>
        /// <param name="createRoleDTO">Rol bilgileri / Role information</param>
        /// <returns>Oluşturma sonucu / Creation result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO createRoleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roleService.CreateRoleAsync(createRoleDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Rolü günceller / Updates role
        /// </summary>
        /// <param name="id">Rol ID'si / Role ID</param>
        /// <param name="updateRoleDTO">Güncelleme bilgileri / Update information</param>
        /// <returns>Güncelleme sonucu / Update result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] CreateRoleDTO updateRoleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roleService.UpdateRoleAsync(id, updateRoleDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Rolü siler / Deletes role
        /// </summary>
        /// <param name="id">Rol ID'si / Role ID</param>
        /// <returns>Silme sonucu / Delete result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Rolün kullanıcılarını getirir / Gets role users
        /// </summary>
        /// <param name="roleId">Rol ID'si / Role ID</param>
        /// <returns>Kullanıcı ID listesi / User ID list</returns>
        [HttpGet("{roleId}/users")]
        public async Task<IActionResult> GetRoleUsers(string roleId)
        {
            var result = await _roleService.GetRoleUsersAsync(roleId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}


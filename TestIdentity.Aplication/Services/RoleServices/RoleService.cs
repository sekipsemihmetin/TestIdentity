using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestIdentity.Aplication.DTOs.RoleDTOs;
using TestIdentity.Domain.Entities;
using TestIdentity.Domain.Utilities.Concretes;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.Aplication.Services.RoleServices
{
    /// <summary>
    /// Role Service - Rol servisi implementasyonu
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Tüm rolleri getirir / Gets all roles
        /// </summary>
        public async Task<IDataResult<List<RoleDTO>>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDTOs = roles.Select(r => new RoleDTO
            {
                Id = r.Id,
                Name = r.Name ?? "",
                Description = r.Description
            }).ToList();

            return new SuccessDataResult<List<RoleDTO>>(roleDTOs, "Roller başarıyla getirildi");
        }

        /// <summary>
        /// ID'ye göre rol getirir / Gets role by ID
        /// </summary>
        public async Task<IDataResult<RoleDTO>> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return new ErrorDataResult<RoleDTO>(null, "Rol bulunamadı");
            }

            var roleDTO = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name ?? "",
                Description = role.Description
            };

            return new SuccessDataResult<RoleDTO>(roleDTO, "Rol başarıyla getirildi");
        }

        /// <summary>
        /// Yeni rol oluşturur / Creates new role
        /// </summary>
        public async Task<IResult> CreateRoleAsync(CreateRoleDTO createRoleDTO)
        {
            var existingRole = await _roleManager.FindByNameAsync(createRoleDTO.Name);
            if (existingRole != null)
            {
                return new ErrorResult("Bu rol adı zaten kullanılıyor");
            }

            var role = new ApplicationRole
            {
                Name = createRoleDTO.Name,
                Description = createRoleDTO.Description,
                CreatedDate = DateTime.UtcNow
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Rol oluşturulamadı: {errors}");
            }

            return new SuccessResult("Rol başarıyla oluşturuldu");
        }

        /// <summary>
        /// Rolü günceller / Updates role
        /// </summary>
        public async Task<IResult> UpdateRoleAsync(string id, CreateRoleDTO updateRoleDTO)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return new ErrorResult("Rol bulunamadı");
            }

            var existingRole = await _roleManager.FindByNameAsync(updateRoleDTO.Name);
            if (existingRole != null && existingRole.Id != id)
            {
                return new ErrorResult("Bu rol adı zaten kullanılıyor");
            }

            role.Name = updateRoleDTO.Name;
            role.Description = updateRoleDTO.Description;
            role.NormalizedName = updateRoleDTO.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Rol güncellenemedi: {errors}");
            }

            return new SuccessResult("Rol başarıyla güncellendi");
        }

        /// <summary>
        /// Rolü siler / Deletes role
        /// </summary>
        public async Task<IResult> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return new ErrorResult("Rol bulunamadı");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name ?? "");
            if (usersInRole.Any())
            {
                return new ErrorResult("Bu role sahip kullanıcılar olduğu için rol silinemez");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Rol silinemedi: {errors}");
            }

            return new SuccessResult("Rol başarıyla silindi");
        }

        /// <summary>
        /// Rolün kullanıcılarını getirir / Gets role users
        /// </summary>
        public async Task<IDataResult<List<string>>> GetRoleUsersAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return new ErrorDataResult<List<string>>(new List<string>(), "Rol bulunamadı");
            }

            var users = await _userManager.GetUsersInRoleAsync(role.Name ?? "");
            var userIds = users.Select(u => u.Id).ToList();

            return new SuccessDataResult<List<string>>(userIds, "Rol kullanıcıları başarıyla getirildi");
        }
    }
}


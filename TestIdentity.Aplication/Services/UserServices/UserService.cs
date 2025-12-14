using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestIdentity.Aplication.DTOs.UserDTOs;
using TestIdentity.Domain.Entities;
using TestIdentity.Domain.Utilities.Concretes;
using TestIdentity.Domain.Utilities.Interfaces;

namespace TestIdentity.Aplication.Services.UserServices
{
    /// <summary>
    /// User Service - Kullanıcı servisi implementasyonu
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Tüm kullanıcıları getirir / Gets all users
        /// </summary>
        public async Task<IDataResult<List<UserDTO>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName ?? "",
                    Email = user.Email ?? "",
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    Roles = roles.ToList()
                });
            }

            return new SuccessDataResult<List<UserDTO>>(userDTOs, "Kullanıcılar başarıyla getirildi");
        }

        /// <summary>
        /// ID'ye göre kullanıcı getirir / Gets user by ID
        /// </summary>
        public async Task<IDataResult<UserDTO>> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ErrorDataResult<UserDTO>(null, "Kullanıcı bulunamadı");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                Roles = roles.ToList()
            };

            return new SuccessDataResult<UserDTO>(userDTO, "Kullanıcı başarıyla getirildi");
        }

        /// <summary>
        /// Kullanıcı günceller / Updates user
        /// </summary>
        public async Task<IResult> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            var user = await _userManager.FindByIdAsync(updateUserDTO.Id);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.PhoneNumber = updateUserDTO.PhoneNumber;
            user.UpdatedDate = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Kullanıcı güncellenemedi: {errors}");
            }

            return new SuccessResult("Kullanıcı başarıyla güncellendi");
        }

        /// <summary>
        /// Kullanıcıyı siler (soft delete) / Deletes user (soft delete)
        /// </summary>
        public async Task<IResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            user.IsActive = false;
            user.UpdatedDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new SuccessResult("Kullanıcı başarıyla silindi");
        }

        /// <summary>
        /// Kullanıcıyı aktif/pasif yapar / Activates/deactivates user
        /// </summary>
        public async Task<IResult> ToggleUserStatusAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            user.IsActive = !user.IsActive;
            user.UpdatedDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new SuccessResult($"Kullanıcı {(user.IsActive ? "aktif" : "pasif")} edildi");
        }

        /// <summary>
        /// Kullanıcının rollerini getirir / Gets user roles
        /// </summary>
        public async Task<IDataResult<List<string>>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorDataResult<List<string>>(new List<string>(), "Kullanıcı bulunamadı");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return new SuccessDataResult<List<string>>(roles.ToList(), "Roller başarıyla getirildi");
        }

        /// <summary>
        /// Kullanıcıya rol atar / Assigns role to user
        /// </summary>
        public async Task<IResult> AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return new ErrorResult("Rol bulunamadı");
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                return new ErrorResult("Kullanıcı zaten bu role sahip");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Rol atama başarısız: {errors}");
            }

            return new SuccessResult("Rol başarıyla atandı");
        }

        /// <summary>
        /// Kullanıcıdan rol kaldırır / Removes role from user
        /// </summary>
        public async Task<IResult> RemoveRoleFromUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                return new ErrorResult("Kullanıcı bu role sahip değil");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ErrorResult($"Rol kaldırma başarısız: {errors}");
            }

            return new SuccessResult("Rol başarıyla kaldırıldı");
        }
    }
}


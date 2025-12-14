using Microsoft.AspNetCore.Identity;
using System;

namespace TestIdentity.Domain.Entities
{
    /// <summary>
    /// Application User Entity - Kullanıcı bilgilerini tutan entity / Entity that holds user information
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Kullanıcının adı / User's first name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının soyadı / User's last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının doğum tarihi / User's birth date
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Kullanıcının profil resmi URL'i / User's profile picture URL
        /// </summary>
        public string? ProfilePictureUrl { get; set; }

        /// <summary>
        /// Kullanıcının oluşturulma tarihi / User creation date
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Kullanıcının son güncellenme tarihi / User last update date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Kullanıcının aktif olup olmadığı / Whether the user is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Refresh token / Yenileme token'ı
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Refresh token'ın sona erme tarihi / Refresh token expiration date
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}


using Microsoft.AspNetCore.Identity;
using System;

namespace TestIdentity.Domain.Entities
{
    /// <summary>
    /// Application Role Entity - Rol bilgilerini tutan entity / Entity that holds role information
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// Rolün açıklaması / Role description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Rolün oluşturulma tarihi / Role creation date
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}


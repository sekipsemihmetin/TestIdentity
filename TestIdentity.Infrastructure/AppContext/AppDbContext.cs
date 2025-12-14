using TestIdentity.Domain.Core.BaseEntites;
using TestIdentity.Domain.Entities;
using TestIdentity.Infrastructure.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure.AppContext
{
    /// <summary>
    /// Application Database Context - Veritabanı bağlamı / Database context
    /// </summary>
    public class AppDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public const string DevConnectionString = "AppConnectionDev";

        //private readonly IHttpContextAccessor _contextAccessor;

        //public AppDbContext(IHttpContextAccessor contextAccessor)
        //{
        //    _contextAccessor = contextAccessor;
        //}

        public AppDbContext( DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public virtual DbSet<Test> Tests {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            SetBaseProperties();
            return base.SaveChanges();
        }

        private void SetBaseProperties()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var userId = "UserBulunamadı";
            foreach (var entry in entries)
            {
                SetIfAdded(entry, userId);
                SetIfModified(entry, userId);
                SetIfDeleted(entry, userId);

            }
        }

        private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State != EntityState.Deleted)
            {
                return;
            }
            entry.State = EntityState.Modified;
            entry.Entity.Status = TestIdentity.Domain.Enums.Status.Deleted;
            entry.Entity.UpdatedDate = DateTime.UtcNow;
            entry.Entity.UpdatedBy = userId;
        }

        private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.Status = TestIdentity.Domain.Enums.Status.Active;
                entry.Entity.UpdatedDate = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;

            }
        }

        private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Status = TestIdentity.Domain.Enums.Status.Active;
                entry.Entity.CreatedDate = DateTime.UtcNow;
                entry.Entity.CreatedBy = userId;
                entry.Entity.UpdatedDate = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;

            }
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);
        }



    }
}

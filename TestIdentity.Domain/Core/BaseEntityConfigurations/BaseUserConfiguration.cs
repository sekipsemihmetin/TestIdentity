using TestIdentity.Domain.Core.BaseEntites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Domain.Core.BaseEntityConfigurations
{
    public class BaseUserConfiguration<TEntity> :BaseEntityConfiguration<TEntity> where TEntity : BaseUser
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(b=>b.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(b=>b.LastName).IsRequired().HasMaxLength(128);
            builder.Property(b=>b.Email).IsRequired().HasMaxLength(128);

            base.Configure(builder);
        }
    }
}

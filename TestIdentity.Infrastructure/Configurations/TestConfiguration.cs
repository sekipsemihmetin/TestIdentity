using TestIdentity.Domain.Core.BaseEntityConfigurations;
using TestIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure.Configurations
{
    public class TestConfiguration:BaseEntityConfiguration<Test>
    {
        public override void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.Property(t => t.Name).IsRequired().HasMaxLength(150);

            base.Configure(builder);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.API2.Core.Entities;

namespace IdentityServer.API2.Data.Configurations.EntityConfigurations
{
    public class ImageQualityConfiguration : IEntityTypeConfiguration<ImageQuality>
    {
        public void Configure(EntityTypeBuilder<ImageQuality> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.Rate).HasMaxLength(5);
            builder.Property(x => x.Rate).HasMaxLength(100);
        }
    }
}

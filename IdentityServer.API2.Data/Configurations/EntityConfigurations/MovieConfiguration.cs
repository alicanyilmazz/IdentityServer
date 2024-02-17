using IdentityServer.API2.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Data.Configurations.EntityConfigurations
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ReleaseDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.Score).HasDefaultValue(0).HasMaxLength(100);
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
        }
    }
}

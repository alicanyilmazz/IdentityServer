using IdentityServer.API2.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Data.Configurations.EntityConfigurations
{
    public class ImageFileDetailConfiguration : IEntityTypeConfiguration<ImageFileDetail>
    {
        public void Configure(EntityTypeBuilder<ImageFileDetail> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

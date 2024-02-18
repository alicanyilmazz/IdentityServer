using IdentityServer.API2.Core.Dtos.StoredProcedureDtos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Data.Configurations.EntityConfigurations
{
    internal class ImageQualityResultConfiguration : IEntityTypeConfiguration<ImageQualityResponse>
    {
        public void Configure(EntityTypeBuilder<ImageQualityResponse> builder)
        {
            builder.HasNoKey();
        }
    }
}

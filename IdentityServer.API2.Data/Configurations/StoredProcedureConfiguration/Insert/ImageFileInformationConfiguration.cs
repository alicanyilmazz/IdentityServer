using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.API2.Core.Dtos.StoredProcedureDtos;

namespace IdentityServer.API2.Data.Configurations.StoredProcedureConfiguration.Insert
{
    public class ImageFileInformationConfiguration : IEntityTypeConfiguration<ImageFileInformation>
    {
        public void Configure(EntityTypeBuilder<ImageFileInformation> builder)
        {
            builder.HasNoKey();
            builder.Ignore(x => x.Type);
        }
    }
}

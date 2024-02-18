using IdentityServer.API2.Core.Entities;
using IdentityServer.API2.Core.Services;
using IdentityServer.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Core.Abstract
{
    public interface IImageServerSaveManager
    {
        public Task<Response<NoDataDto>> Save(IImageServerSaveService services, IEnumerable<ImageDbServiceRequest> images, string directory);

    }
}

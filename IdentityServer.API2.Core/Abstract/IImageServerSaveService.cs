using IdentityServer.API2.Core.Entities;
using IdentityServer.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Core.Abstract
{
    public interface IImageServerSaveService
    {
        public Task<Response<NoDataDto>> SaveAsync(IEnumerable<ImageDbServiceRequest> images, string directory);
    }
}

using IdentityServer.API2.Core.Dtos;
using IdentityServer.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Core.Abstract
{
    public interface IImageServerReadService
    {
        public Task<Response<IEnumerable<ImageServerServiceResponse>>> GetPhotosAsync();
        public Task<Response<IEnumerable<ImageServerServiceResponse>>> GetPhotoAsync(string imageId);
    }
}

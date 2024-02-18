using IdentityServer.API2.Core.Abstract;
using IdentityServer.API2.Core.Entities;
using IdentityServer.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Service.Services.ImageSaveService.Server.Managers
{
    public class ImageServerSaveManager : IImageServerSaveManager
    {
        public async Task<Response<NoDataDto>> Save(IImageServerSaveService services, IEnumerable<ImageDbServiceRequest> images, string directory)
        {
            return await services.SaveAsync(images, directory);
        }
    }
}

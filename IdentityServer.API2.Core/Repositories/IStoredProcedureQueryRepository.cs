using IdentityServer.API2.Core.Dtos.StoredProcedureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Core.Repositories
{
    public interface IStoredProcedureQueryRepository
    {
        public Task<List<ServerImagesInformation>> GetImage(string imageId);
        public Task<List<ServerImagesInformation>> GetImages();
        public Task<List<ImageQualityResponse>> GetImageQualityConfigs();
        /// ExecuteSqlInterpolatedAsync yöntemi, çıktı parametrelerini doğrudan desteklemez. Bu nedenle, çıktı parametresi kullanırken ExecuteSqlRawAsync yöntemini kullanmanız gerekmektedir.
        /// <summary>
        /// This methods returns number of ImageFile record.
        /// </summary>
        /// <returns>NumberOfImageFile</returns>
        public Task<int> GetNumberOfRecord();
    }
}

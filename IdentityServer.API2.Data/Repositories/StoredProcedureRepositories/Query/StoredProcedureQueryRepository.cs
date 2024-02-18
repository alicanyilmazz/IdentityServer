using IdentityServer.API2.Core.Dtos.StoredProcedureDtos;
using IdentityServer.API2.Core.Repositories;
using IdentityServer.API2.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Data.Repositories.StoredProcedureRepositories.Query
{
    public class StoredProcedureQueryRepository : IStoredProcedureQueryRepository
    {
        private readonly DbContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public StoredProcedureQueryRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        }
        public async Task<List<ServerImagesInformation>> GetImages()
        {
            return await _context.Set<ServerImagesInformation>().FromSqlInterpolated($"EXEC GET_IMAGES").ToListAsync();
        }
        public async Task<List<ServerImagesInformation>> GetImage(string imageId)
        {
            var ImageId = new SqlParameter("ImageId", imageId);
            return await _context.Set<ServerImagesInformation>().FromSqlInterpolated($"EXEC GET_IMAGE {ImageId}").ToListAsync();
        }
        public async Task<List<ImageQualityResponse>> GetImageQualityConfigs()
        {
            return await _context.Set<ImageQualityResponse>().FromSqlInterpolated($"EXEC GET_IMAGE_QUALITY").ToListAsync();
        }
        public async Task<int> GetNumberOfRecord()
        {
            var recordCountParameter = new SqlParameter
            {
                ParameterName = "@RecordCount",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            await _context.Database.ExecuteSqlRawAsync($"EXEC GET_NUMBER_OF_IMAGE_FILE @RecordCount OUTPUT", recordCountParameter);

            return (int)recordCountParameter.Value;
        }   
    }
}

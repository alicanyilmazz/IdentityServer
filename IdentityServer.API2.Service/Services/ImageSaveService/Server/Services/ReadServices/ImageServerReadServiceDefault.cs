﻿using IdentityServer.API2.Core.Abstract;
using IdentityServer.API2.Core.Dtos;
using IdentityServer.API2.Core.Repositories;
using IdentityServer.API2.Service.DtoMappers;
using IdentityServer.SharedLibrary.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Service.Services.ImageSaveService.Server.Services.ReadServices
{
    public class ImageServerReadServiceDefault : IImageServerReadService
    {
        private readonly IStoredProcedureQueryRepository _storedProcedureQueryRepository;
        private readonly IConfiguration _configuration;
        public ImageServerReadServiceDefault(IStoredProcedureQueryRepository storedProcedureQueryRepository, IConfiguration configuration)
        {
            _storedProcedureQueryRepository = storedProcedureQueryRepository;
            _configuration = configuration;
        }
        public async Task<Response<IEnumerable<ImageServerServiceResponse>>> GetPhotosAsync()
        {
            IEnumerable<ImageServerServiceResponse> entities;
            try
            {
                entities = ObjectMapper.Mapper.Map<IEnumerable<ImageServerServiceResponse>>(await _storedProcedureQueryRepository.GetImages());
                var baseUrl =_configuration["Endpoints:ImageServerUrl"];
                foreach (var entity in entities)
                {
                    entity.Path = baseUrl + entity.Path;
                }
            }
            catch (Exception e)
            {
                return Response<IEnumerable<ImageServerServiceResponse>>.Fail(e.Message, 404, true);
            }
            return Response<IEnumerable<ImageServerServiceResponse>>.Success(entities, 200);
        }

        public async Task<Response<IEnumerable<ImageServerServiceResponse>>> GetPhotoAsync(string imageId)
        {
            IEnumerable<ImageServerServiceResponse> entities;
            try
            {
                entities = ObjectMapper.Mapper.Map<IEnumerable<ImageServerServiceResponse>>(await _storedProcedureQueryRepository.GetImage(imageId));
            }
            catch (Exception e)
            {
                return Response<IEnumerable<ImageServerServiceResponse>>.Fail(e.Message, 404, true);
            }
            return Response<IEnumerable<ImageServerServiceResponse>>.Success(entities, 200);
        }
    }
}

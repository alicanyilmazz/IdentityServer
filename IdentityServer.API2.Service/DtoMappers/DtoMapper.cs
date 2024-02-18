using AutoMapper;
using IdentityServer.API2.Core.Dtos;
using IdentityServer.API2.Core.Dtos.StoredProcedureDtos;
using IdentityServer.API2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Service.DtoMappers
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<ServerImagesInformation, ImageServerServiceResponse>().ReverseMap();
        }
    }
}

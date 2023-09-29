using AutoMapper;
using IdentityServer.API1.Core.Dtos;
using IdentityServer.API1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API1.Service.DtoMappers
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}

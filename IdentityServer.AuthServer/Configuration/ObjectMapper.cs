using AutoMapper;


namespace IdentityServer.AuthServer.Configuration
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<IdentityServer4.Models.Client, IdentityServer4.EntityFramework.Entities.Client>().ReverseMap();
        }
    }
}

using AutoMapper;
using AuthenticationServer.Domain.Entities;

namespace AuthenticationServer.Repository
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthenticationServer.Domain.Entities.User, AuthenticationServer.Domain.Models.UserDto>();
            }).CreateMapper();
    }
}
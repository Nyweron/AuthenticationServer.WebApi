using AuthenticationServer.WebApi.Models;
using AuthenticationServer.WebApi.Entities;
using AutoMapper;

namespace AuthenticationServer.WebApi
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            }).CreateMapper();
    }
}
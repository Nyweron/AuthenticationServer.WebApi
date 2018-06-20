using AuthenticationServer.WebApi.Models;
using AuthenticationServer.WebApi.Entities;
using AutoMapper;

namespace AuthenticationServer.WebApi.Settings
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto ,User>();
            }).CreateMapper();
    }
}
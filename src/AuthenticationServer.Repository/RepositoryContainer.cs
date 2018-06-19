using AuthenticationServer.Repository.User;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace AuthenticationServer.Repository
{
    public static class RepositoryContainer
    {
        public static void Update(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(RepositoryContainer).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>();
        }
    }
}
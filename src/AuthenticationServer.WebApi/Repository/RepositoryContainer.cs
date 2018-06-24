using AuthenticationServer.WebApi.Settings;
using Autofac;

namespace AuthenticationServer.WebApi.Repository
{
  public static class RepositoryContainer
  {
    public static void Update(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(RepositoryContainer).Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
      builder.RegisterInstance(AutoMapperConfig.GetMapper())
        .SingleInstance();
    }
  }
}
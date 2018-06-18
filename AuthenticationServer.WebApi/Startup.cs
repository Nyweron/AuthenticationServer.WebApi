using AuthenticationServer.Data;
using AuthenticationServer.Domain.Entities;
using AuthenticationServer.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace AuthenticationServer.WebApi
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional : true, reloadOnChange : true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Contacts API", Version = "v1" });
            });

            var connectionString = Startup.Configuration["ConnectionStrings:AuthenticationServerConnection"];
            services.AddDbContext<AuthenticationServerDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            //http://localhost:5000/swagger/
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.UserDto, User>();
            });

            app.UseMvc();
        }
    }
}
﻿using System;
using AuthenticationServer.Data;
using AuthenticationServer.Data.EF;
using AuthenticationServer.Domain.Entities;
using AuthenticationServer.WebApi.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Contacts API", Version = "v1" });
            });

            services.AddMvc();
            services.AddMemoryCache();
            services.AddResponseCaching();


            services.Configure<SqlOptions>(Configuration.GetSection("sql"));
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<AuthenticationServerDbContext>();


            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            Container = builder.Build();
           //services.AddScoped<IUserRepository, UserRepository>();
            return new AutofacServiceProvider(Container);

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

            app.UseResponseCaching();
            app.UseMvc();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
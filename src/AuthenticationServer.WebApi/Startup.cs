﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AuthenticationServer.WebApi.Data;
using AuthenticationServer.WebApi.Repository;
using AuthenticationServer.WebApi.Settings.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

            services.Configure<DatabaseOptions>(Configuration.GetSection("sql"));
            services.Configure<JwtOptions>(Configuration.GetSection("jwt"));
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<AuthenticationServerDbContext>();

            services.AddAuthorization(a => a.AddPolicy("admin", policy => policy.RequireRole("admin")));
            services.AddAuthorization(b => b.AddPolicy("user", policyb => policyb.RequireRole("user")));

            var jwtOptions = new JwtOptions();
            Configuration.GetSection("jwt").Bind(jwtOptions);
            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                });

            // Configute Autofac
            var builder = new ContainerBuilder();
            // Loads the already configured items from services object
            builder.Populate(services);

            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            RepositoryContainer.Update(builder);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime applicationLifetime)
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

            app.UseAuthentication();
            app.UseResponseCaching();
            app.UseMvc();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
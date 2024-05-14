using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Application.Common;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application;
using Infrastructure;
using WebApi.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using WebApi;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


namespace Project.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
            });

            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddControllers();

            var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();

            services.AddCors(options =>
            {
                var frontendURL = configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(r =>
                {
                    r.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
                });
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            services.AddApiVersioning();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                foreach(var description in provider.ApiVersionDescriptions)
                {
                    cfg.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    cfg.RoutePrefix = string.Empty;
                }
                cfg.RoutePrefix = string.Empty;
                cfg.SwaggerEndpoint("swagger/v1/swagger.json", "Projects API");
            });
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseCors("AllowAll");
            app.UseApiVersioning();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
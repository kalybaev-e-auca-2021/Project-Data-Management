using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
            _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var desctiption in _provider.ApiVersionDescriptions)
            {
                var apiVersion = desctiption.ApiVersion.ToString();
                options.SwaggerDoc(desctiption.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"Projects API {apiVersion}",
                        Description =
                            "A simple example ASP NET Core Web API(Project-Management-Data)",
                        TermsOfService = new Uri("https://github.com/kalybaev-e-auca-2021/Project-Data-Management"),
                        Contact = new OpenApiContact
                        {
                            Name = "",
                            Email = string.Empty,
                            Url = new Uri("https://t.me/thunderer1908")
                        },
                        License = new OpenApiLicense 
                        { 
                            Name = "Erbol Kalybaev",
                            Url = new Uri("https://www.linkedin.com/in/erbol-kalybaev-a69b2b277/")
                        }
                    });
                options.AddSecurityDefinition($"AuthToken{apiVersion}",
                    new OpenApiSecurityScheme
                    { 
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authorization",
                        Description = "Authorization token"
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = $"AuthToken {apiVersion}"
                           }
                       },
                        new string[]{}
                    }
                });
                options.CustomOperationIds(apiDescription =>
                    apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
            }
        }
    }
}

using System.Diagnostics;
using System.Reflection;
using AutoMapper;
using Infrastructure.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProvaDti
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
        {
            ConfigureSwagger(appBuilder);

            if (env.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
            }

            appBuilder
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureSwagger(IApplicationBuilder appBuilder)
        {
            appBuilder
                .UseOpenApi()
                .UseSwaggerUi3()
                .UseReDoc(x => { x.Path = "/ReDoc"; });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiVersion = GetApiVersion();

            services
                .AddOpenApiDocument(config =>
                {
                    config.DocumentName = $"V{apiVersion}";
                    config.PostProcess = document =>
                    {
                        document.Info.Version = apiVersion;
                        document.Info.Title = "DTI - Stock Management";
                        document.Info.Description = "Control your products here.";
                    };
                });

            services.AddControllers();

            InjectAutoMapper(services);

            var connection = Configuration.GetConnectionString("DefaultConnection");
            InjectorContainer.Register(services, connection);
        }

        private static void InjectAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(new[] {
                    "Application"
                });
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            mappingConfig.AssertConfigurationIsValid();
        }

        private static string GetApiVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}
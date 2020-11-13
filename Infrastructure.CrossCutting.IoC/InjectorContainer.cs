using Application.Domain.Services;
using Application.Infrastructure.Data.Context;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCutting.IoC
{
    public static class InjectorContainer
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddEntityFrameworkProxies()
                    .BuildServiceProvider();

                services.AddDbContext<MyContext>(options => options
                    .UseInternalServiceProvider(serviceProvider)
                    .UseInMemoryDatabase("InMemoryDb")
                    .UseLazyLoadingProxies()
                    .EnableSensitiveDataLogging());
            }
            else
            {
                services.AddDbContext<MyContext>(options =>
                    options
                        .UseSqlite(connectionString)
                        .UseLazyLoadingProxies()
                );
            }

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
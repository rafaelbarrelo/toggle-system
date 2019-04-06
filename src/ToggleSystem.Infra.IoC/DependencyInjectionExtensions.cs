using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToggleSystem.Domain.Interfaces.Repositories;
using ToggleSystem.Domain.Interfaces.Services;
using ToggleSystem.Domain.Services;
using ToggleSystem.Infra.Data.Context;
using ToggleSystem.Infra.Data.Repositories;

namespace ToggleSystem.Infra.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ToggleContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .RegistrerServices()
                .RegistrerRepositories();
                
            return services;
        }

        private static IServiceCollection RegistrerServices(this IServiceCollection services)
        {
            services.AddScoped<IToggleService, ToggleService>();

            return services;
        }

        private static IServiceCollection RegistrerRepositories(this IServiceCollection services)
        {
            services.AddScoped<IToggleRepository, ToggleRepository>();

            return services;
        }
    }
}

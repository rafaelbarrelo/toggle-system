using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToggleSystem.Infra.Data.Context;

namespace ToggleSystem.Infra.IoC
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ToggleContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}

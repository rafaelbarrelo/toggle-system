using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ToggleSystem.Infra.Identity.Interfaces;

namespace ToggleSystem.Infra.Identity.Seed
{
    public static class SecurityDataSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var securityService = serviceProvider.GetRequiredService<ISecurityService>();

            var canGetToggle = new Claim("Toggle", "Get");
            var canManage = new Claim("Toggle", "Manage");

            if (!securityService.UserExists("admin"))
            {
                await securityService.AddNewUser("admin", "admin", new[] { canGetToggle, canManage });
            }

            if (!securityService.UserExists("application1"))
            {
                await securityService.AddNewUser("application1", "1234", new[] { canGetToggle });
            }

            if (!securityService.UserExists("application2"))
            {
                await securityService.AddNewUser("application2", "4321", new[] { canGetToggle });
            }
        }
    }
}

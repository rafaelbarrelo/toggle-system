using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToggleSystem.Infra.Identity.Entities;

namespace ToggleSystem.Infra.Identity.Interfaces
{
    public interface ISecurityService
    {
        Task AddNewUser(string username, string password, IEnumerable<Claim> claims);
        bool UserExists(string username);
        Task<bool> SignIn(string username, string password);
        Task<ApiToken> AuthorizeAsync(string username, string password);
    }
}

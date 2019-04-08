using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToggleSystem.Infra.Identity.Entities;
using ToggleSystem.Infra.Identity.Interfaces;

namespace ToggleSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public AccountController(ISecurityService securityService) => _securityService = securityService;

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var token = await _securityService.AuthorizeAsync(model.User, model.Password);

            if (token != null)
            {
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}

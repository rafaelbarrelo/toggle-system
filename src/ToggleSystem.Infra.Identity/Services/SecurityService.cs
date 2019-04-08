using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToggleSystem.Infra.Identity.Entities;
using ToggleSystem.Infra.Identity.Interfaces;

namespace ToggleSystem.Infra.Identity.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public SecurityService(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager,
                               IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task AddNewUser(string username, string password, IEnumerable<Claim> claims)
        {
            var user = new IdentityUser { UserName = username };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                // Log error
                return;
            }

            var claimsResult = await _userManager.AddClaimsAsync(user, claims);
            if (!claimsResult.Succeeded)
            {
                // Log error
            }

        }

        public async Task<bool> SignIn(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            return result.Succeeded;
        }

        public async Task<ApiToken> AuthorizeAsync(string username, string password)
        {
            try
            {
                var signInSuccess = await SignIn(username, password);

                if (!signInSuccess)
                {
                    return null;
                }

                var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);

                var claims = await _userManager.GetClaimsAsync(user);

                return await GenerateJwtTokenAsync(user, claims);
            }
            catch (Exception)
            {
                // Log error
            }

            return null;
        }

        private async Task<ApiToken> GenerateJwtTokenAsync(IdentityUser user, IList<Claim> claims = null)
        {
            var _claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            if (claims != null)
            {
                _claims.AddRange(claims);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresSeconds = Convert.ToInt32(_configuration["Security:JwtExpireSeconds"]);

            var token = new JwtSecurityToken(
                _configuration["Security:JwtIssuer"],
                _configuration["Security:JwtIssuer"],
                _claims,
                expires: DateTime.Now.AddSeconds(expiresSeconds),
                signingCredentials: creds
            );

            return await Task.FromResult(new ApiToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = expiresSeconds
            });
        }

        public bool UserExists(string username)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            return user != null;
        }
    }
}


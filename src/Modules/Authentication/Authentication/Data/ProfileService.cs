using Authentication.Models;
using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authentication.Data
{
    public class ProfileService : IProfileService
    {

        private readonly UserManager<Models.User> _userManager;
        private readonly IConfiguration _configuration;
        public ProfileService(UserManager<Models.User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());

            var issuer = _configuration.GetSection("IdentityServer").Value;

            if (user == null) return;

            var claims = new List<Claim>
            {
                
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Name, user.UserName),
                new Claim(JwtClaimTypes.Id, user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}

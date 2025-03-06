
using Authentication.Models;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;


namespace Authentication.Data
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<User> _userManager;

        public ResourceOwnerPasswordValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, context.Password);
                if (result)
                {
                    context.Result = new GrantValidationResult(user.Id.ToString(), "password");
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid_password");
                }
            }
            else
            {
                // User not found
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid_username");
            }
        }
    }
}

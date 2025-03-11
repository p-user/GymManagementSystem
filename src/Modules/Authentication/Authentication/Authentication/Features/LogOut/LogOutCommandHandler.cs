using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Authentication.Authentication.Features.LogOut
{
    public record LogOutCommand(string Token) : IRequest<bool>;
    public class LogOutCommandHandler(IHttpContextAccessor _httpContextAccessor, IHttpClientFactory _httpClient, IDiscoveryService _discoveryService) : IRequestHandler<LogOutCommand, bool>
    {
        public async Task<bool> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            await context.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            var client = _httpClient.CreateClient();
            if (!string.IsNullOrEmpty(request.Token))
            {
                var doc = await _discoveryService.GetDiscoveryDocumentAsync(cancellationToken);

                if (doc.IsError)
                {
                    throw new Exception("Failed to retrieve IdentityServer discovery document.");
                }
                var pswtokenRevokeRequest = new TokenRevocationRequest
                {
                    Address = doc.TokenEndpoint,
                    ClientId = _discoveryService.GetClientCredentials().ClientId,
                    ClientSecret = _discoveryService.GetClientCredentials().ClientSecret,
                    Token = request.Token

                };
                var tokenRevokeResponse = await client.RevokeTokenAsync(pswtokenRevokeRequest);

                if (tokenRevokeResponse.IsError)
                {
                    throw new Exception("Failed to revoke refresh token: " + tokenRevokeResponse.Error);
                }
            }

            return true;
        }
    }
}

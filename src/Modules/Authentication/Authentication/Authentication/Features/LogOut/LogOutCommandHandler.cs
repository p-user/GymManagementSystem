namespace Authentication.Authentication.Features.LogOut
{
    public record LogOutCommand(string Token) : IRequest<Shared.Results.Results>;
    public class LogOutCommandHandler(IHttpContextAccessor _httpContextAccessor, IHttpClientFactory _httpClient, IDiscoveryService _discoveryService) : IRequestHandler<LogOutCommand, Shared.Results.Results>
    {
        public async Task<Shared.Results.Results> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            await context.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            var client = _httpClient.CreateClient();
            if (!string.IsNullOrEmpty(request.Token))
            {
                var doc = await _discoveryService.GetDiscoveryDocumentAsync(cancellationToken);

                if (doc.IsError)
                {
                    var error = new Error("141", doc.Error, ErrorType.Failure);
                    return Shared.Results.Results.Failure(error);

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
                    var error = new Error(tokenRevokeResponse.HttpStatusCode.ToString(), tokenRevokeResponse.Error, ErrorType.Failure);
                    return Shared.Results.Results.Failure(error);

                }
            }

            return Shared.Results.Results.Success();
        }
    }
}

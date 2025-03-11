namespace Authentication.Authentication.Features.RefreshToken
{
    public record RefreshTokenCommand(string Token) : IRequest<RefreshTokenResponse>;
    public record RefreshTokenResponse(string AccessToken, string TokenType, string RefreshToken, int ExpiresIn);


    public class RefreshTokenCommandHandler(IHttpClientFactory _httpClientFactory, IDiscoveryService _discoveryService, IConfiguration _config) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //duende server 
            var doc = await _discoveryService.GetDiscoveryDocumentAsync(cancellationToken);

            var client = _httpClientFactory.CreateClient();

            var pswtokenRequest = new RefreshTokenRequest
            {
                Address = doc.TokenEndpoint,
                ClientId = _discoveryService.GetClientCredentials().ClientId,
                ClientSecret = _discoveryService.GetClientCredentials().ClientSecret,
                Scope = string.Join(" ", _discoveryService.GetClientCredentials().Scopes),
                RefreshToken = request.Token,
            };

            var token = await client.RequestRefreshTokenAsync(pswtokenRequest);

            return new RefreshTokenResponse(token.AccessToken, token.TokenType, token.RefreshToken, token.ExpiresIn);
        }
    }
}


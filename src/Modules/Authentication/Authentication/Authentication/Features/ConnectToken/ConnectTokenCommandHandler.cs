
namespace Authentication.Authentication.Features.ConnectToken
{
    public record ConnectTokenRequest(RequestTokenDto requestToken) : IRequest<ConnectTokenResponse>;
    public record ConnectTokenResponse(string AccessToken, string TokenType, string RefreshToken, int ExpiresIn);
    public class ConnectTokenCommandHandler(IHttpClientFactory _httpClientFactory,IDiscoveryService _discoveryService, IConfiguration _config) : IRequestHandler<ConnectTokenRequest, ConnectTokenResponse>
    {
        public async Task<ConnectTokenResponse> Handle(ConnectTokenRequest request, CancellationToken cancellationToken)
        {

            //duende server 
            var doc = await _discoveryService.GetDiscoveryDocumentAsync(cancellationToken);
          
            var client = _httpClientFactory.CreateClient();

            var pswtokenRequest = new PasswordTokenRequest
            {
                Address = doc.TokenEndpoint,
                ClientId = _discoveryService.GetClientCredentials().ClientId,
                ClientSecret = _discoveryService.GetClientCredentials().ClientSecret,
                Scope = string.Join(" ", _discoveryService.GetClientCredentials().Scopes),
                UserName = request.requestToken.Email,
                Password = request.requestToken.Password,
            };

            var token = await client.RequestPasswordTokenAsync(pswtokenRequest);

            return new ConnectTokenResponse(token.AccessToken, token.TokenType, token.RefreshToken, token.ExpiresIn);

        }
    }
}

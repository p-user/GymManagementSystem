

namespace Authentication.Authentication.Features.ConnectToken
{
    public record ConnectTokenRequest(RequestTokenDto requestToken) : IRequest<Results<RequestTokenResponseDto>>;
    public class ConnectTokenCommandHandler(IHttpClientFactory _httpClientFactory, IDiscoveryService _discoveryService, IConfiguration _config) : IRequestHandler<ConnectTokenRequest, Results<RequestTokenResponseDto>>
    {
        public async Task<Results<RequestTokenResponseDto>> Handle(ConnectTokenRequest request, CancellationToken cancellationToken)
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

            if (token.IsError)
            {
                var error = new Error(token.HttpStatusCode.ToString(), token.Error, ErrorType.Failure);
                return (Results<RequestTokenResponseDto>)Shared.Results.Results.Failure(error);
            }

            return new RequestTokenResponseDto
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType,
                RefreshToken = token.RefreshToken,
                ExpiresIn = token.ExpiresIn
            };

        }
    }
}



namespace Authentication.Authentication.Features.CurrentUser
{
    public record GetCurrentLoggedInUserQuery(string accessToken) : IRequest<Results<GetCurrentLoggedInUserResponseDto>>;
    public class GetCurrentLoggedInUserQueryHandler(IDiscoveryService _discoveryService, HttpClient httpClient) : IRequestHandler<GetCurrentLoggedInUserQuery, Results<GetCurrentLoggedInUserResponseDto>>
    {
        public async Task<Results<GetCurrentLoggedInUserResponseDto>> Handle(GetCurrentLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            var doc = await _discoveryService.GetDiscoveryDocumentAsync(cancellationToken);
            if (doc.IsError)
            {
                throw new Exception("Failed to retrieve discovery document: " + doc.Error);
            }

            var userInfoResponse = await httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = doc.UserInfoEndpoint,
                Token = request.accessToken
            });

            var response = new GetCurrentLoggedInUserResponseDto
              (
                  userInfoResponse.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
                  userInfoResponse.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                  userInfoResponse.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
                  userInfoResponse.Claims.Where(c => c.Type == "role").Select(c => c.Value).First()
              );

            return response;
        }
    }
}


namespace Authentication.Authentication.Features.CurrentUser
{
    public record GetCurrentLoggedInUserQuery(string accessToken) : IRequest<GetCurrentLoggedInUserResponse>;
    public record GetCurrentLoggedInUserResponse(string Sub,string Email, string UserName, string Role);
    public class GetCurrentLoggedInUserQueryHandler(IDiscoveryService _discoveryService, HttpClient httpClient) : IRequestHandler<GetCurrentLoggedInUserQuery,GetCurrentLoggedInUserResponse>
    {
        public async Task<GetCurrentLoggedInUserResponse> Handle(GetCurrentLoggedInUserQuery request, CancellationToken cancellationToken)
        {
           var doc =await _discoveryService.GetDiscoveryDocumentAsync(cancellationToken);
            if (doc.IsError)
            {
                throw new Exception("Failed to retrieve discovery document: " + doc.Error);
            }

            var userInfoResponse = await httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = doc.UserInfoEndpoint,
                Token = request.accessToken
            });

          var response =  new  GetCurrentLoggedInUserResponse
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

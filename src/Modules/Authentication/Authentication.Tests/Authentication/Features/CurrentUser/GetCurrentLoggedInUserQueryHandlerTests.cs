
using Authentication.Authentication.Features.CurrentUser;
using Authentication.Services;
using Duende.IdentityModel.Client;
using System.Security.Claims;

namespace Authentication.Tests.Authentication.Features.CurrentUser
{
    public class GetCurrentLoggedInUserQueryHandlerTests
    {
        private readonly Mock<IDiscoveryService> _mockDiscoveryService;
        private readonly Mock<HttpClient> _mockHttpClient;
        private readonly GetCurrentLoggedInUserQueryHandler _handler;
        private readonly string _accessToken = "sample_access_token";
        private readonly CancellationToken _cancellationToken = new CancellationToken();

        public GetCurrentLoggedInUserQueryHandlerTests()
        {
            _mockDiscoveryService = new Mock<IDiscoveryService>();
            _mockHttpClient = new Mock<HttpClient>();
            _handler = new GetCurrentLoggedInUserQueryHandler(_mockDiscoveryService.Object, _mockHttpClient.Object); 
        }

    }
}

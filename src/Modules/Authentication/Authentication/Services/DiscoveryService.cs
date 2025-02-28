
using Duende.IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace Authentication.Services
{
    public class DiscoveryService : IDiscoveryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _identityServerUrl;
        private DiscoveryDocumentResponse _discoveryDocument;


        public DiscoveryService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _identityServerUrl = configuration.GetValue<string>("IdentityServer:Authority");

        }
        public async  Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(CancellationToken cancellationToken)
        {
            //prevent repetitive calls
            if (_discoveryDocument != null)
                return _discoveryDocument;

            var client = _httpClientFactory.CreateClient();
            var discovery = await client.GetDiscoveryDocumentAsync(_identityServerUrl);

            if (discovery.IsError)
                throw new HttpRequestException($"Error retrieving discovery document: {discovery.Error}");

            _discoveryDocument = discovery;
            return _discoveryDocument;
        }
    }
}

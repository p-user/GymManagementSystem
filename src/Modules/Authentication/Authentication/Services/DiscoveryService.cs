
namespace Authentication.Services
{
    public class DiscoveryService : IDiscoveryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _identityServerUrl;
        private DiscoveryDocumentResponse _discoveryDocument;
        private readonly SemaphoreSlim _semaphore = new(1, 1); // Limit to one concurrent request
        private readonly ILogger<DiscoveryService> _logger;


        public DiscoveryService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DiscoveryService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _identityServerUrl = configuration.GetValue<string>("IdentityServer");
            _logger = logger;

        }
        public async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(CancellationToken cancellationToken)
        {
            //prevent repetitive calls
            if (_discoveryDocument != null) return _discoveryDocument;

            await _semaphore.WaitAsync(cancellationToken); // Ensure only one request at a
            try
            {
                if (_discoveryDocument != null) return _discoveryDocument;

                var client = _httpClientFactory.CreateClient();
                var discovery = await client.GetDiscoveryDocumentAsync(_identityServerUrl);
                if (discovery.IsError)
                    throw new HttpRequestException($"Error retrieving discovery document: {discovery.Error}");

                _discoveryDocument = discovery;
                _logger.LogInformation("Discovery document retrieved successfully.");
            }
            finally
            {
                _semaphore.Release();
            }
            return _discoveryDocument;
        }

        public (string ClientId, string ClientSecret, string[] Scopes) GetClientCredentials()
        {
            var identityClient = Clients.GetClients().FirstOrDefault();
            if (identityClient == null)
                throw new InvalidOperationException("Invalid client configuration.");

            return (identityClient.ClientId, ConfigurationConstants.ClientSecret, identityClient.AllowedScopes.ToArray());
        }


    }
}

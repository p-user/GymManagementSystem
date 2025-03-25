namespace Authentication.Services
{
    public interface IDiscoveryService
    {
        Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(CancellationToken cancellationToken);
        (string ClientId, string ClientSecret, string[] Scopes) GetClientCredentials();
    }
}

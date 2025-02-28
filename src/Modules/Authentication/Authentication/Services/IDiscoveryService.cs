
using Duende.IdentityModel.Client;

namespace Authentication.Services
{
    public interface IDiscoveryService
    {
        Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(CancellationToken cancellationToken);
    }
}

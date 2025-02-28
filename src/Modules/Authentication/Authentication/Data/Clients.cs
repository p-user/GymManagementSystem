using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.Models;
using static Duende.IdentityModel.OidcConstants;

namespace Authentication.Data
{
    public static class Clients
    {
        public static IEnumerable<Duende.IdentityServer.Models.Client> GetClients()
        {
            return new List<Duende.IdentityServer.Models.Client>
            {
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = "gymapp",
                    AllowedGrantTypes = Duende.IdentityServer.Models.GrantTypes.ResourceOwnerPassword, // for username password authentication
                    AllowOfflineAccess = true, // Allows refresh tokens
                    ClientSecrets = { new Duende.IdentityServer.Models.Secret("muhabet_kot".Sha256()) },
                    AllowedScopes = { "openid", "profile", "GYMApi", "offline_access" }
                }
            };
        }


        public static IEnumerable<Duende.IdentityServer.Models.ApiScope> ApiScopes =>
           new Duende.IdentityServer.Models.ApiScope[]
           {
                new Duende.IdentityServer.Models.ApiScope("GYMApi", "Gym API"),
                new Duende.IdentityServer.Models.ApiScope("offline_access", "Offline access")
           };

        public static IEnumerable<Duende.IdentityServer.Models.IdentityResource> GetIdentityResources()
        {
            return new List<Duende.IdentityServer.Models.IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}

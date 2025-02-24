
namespace IntegrationTests.Shared
{
    public class BaseTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient Client;
        public BaseTest(WebApplicationFactory<Program> factory)
        {
            Client = factory.CreateClient();
        }
    }
}

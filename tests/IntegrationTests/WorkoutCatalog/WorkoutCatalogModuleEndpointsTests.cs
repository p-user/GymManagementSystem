namespace IntegrationTests.WorkoutCatalogModule
{
    public class WorkoutCatalogModuleEndpointsTests : BaseTest
    {
        public WorkoutCatalogModuleEndpointsTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_WorkoutCategories_Should_Return_OK()
        {
            var response = await Client.GetAsync("/workout/categories");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        //ToDo Add more tests for the other endpoints aftre implementing Docker
    }
}

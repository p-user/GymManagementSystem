namespace Authentication.Authentication.Features.LogOut
{
    public class LogOutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/authentication/logOut", LogOut)
                 .WithDescription("LogOut endpoint")
                 .WithName("LogOut")
                 .WithTags("Authentication")
                 .Produces<Shared.Results.Results>();
        }

        private async Task<IResult> LogOut(ISender sender, [FromBody] string token)
        {
            Shared.Results.Results response = await sender.Send(new LogOutCommand(token));
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

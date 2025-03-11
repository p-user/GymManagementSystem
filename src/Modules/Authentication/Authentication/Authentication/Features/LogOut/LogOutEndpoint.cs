
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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
                 .Produces<bool>();
        }

        private async Task<IResult> LogOut(ISender sender, [FromBody] string token)
        {
            var response = await sender.Send(new LogOutCommand(token));
            return Results.Ok(response);
        }
    }
}

using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Authentication.Authentication.Features.RefreshToken
{
    public class RefreshTokenEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPost("/authentication/refresh-token", RefreshToken)
                .WithDescription("Refresh  Token endpoint")
                .WithName("RefreshToken")
                .WithTags("Authentication")
                .Produces<RefreshTokenResponse>();
        }

        private async Task<IResult> RefreshToken(ISender sender, [FromBody] string token)
        {
            var response = await sender.Send(new RefreshTokenCommand(token));
            return Results.Ok(response);
        }
    }
}

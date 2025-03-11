
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Authentication.Authentication.Features.CurrentUser
{
    public class GetCurrentLoggedInUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/authentication/current-user", GetCurrentLoggedInUser)
                .WithDescription("Get current logged in user endpoint")
                .WithName("GetCurrentLoggedInUser")
                .WithTags("Authentication")
                .Produces<GetCurrentLoggedInUserResponse>()
                .RequireAuthorization();
        }

        private async Task<IResult> GetCurrentLoggedInUser(ISender sender, HttpContext httpContext)
        {
            var accessToken = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(accessToken))
            {
                return Results.Unauthorized();
            }
            var response = await sender.Send(new GetCurrentLoggedInUserQuery(accessToken));
            return Results.Ok(response);
        }
    }
}


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
                .Produces<Results<GetCurrentLoggedInUserResponseDto>>()
                .RequireAuthorization();
        }

        private async Task<IResult> GetCurrentLoggedInUser(ISender sender, HttpContext httpContext)
        {
            var accessToken = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(accessToken))
            {
                return Microsoft.AspNetCore.Http.Results.Unauthorized();
            }
            Shared.Results.Results<GetCurrentLoggedInUserResponseDto> response = await sender.Send(new GetCurrentLoggedInUserQuery(accessToken));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem); ;
        }
    }
}

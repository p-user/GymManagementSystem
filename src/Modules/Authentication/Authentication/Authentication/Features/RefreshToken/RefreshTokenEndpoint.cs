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
                 .Produces<Results<RefreshTokenResponseDto>>();
        }

        private async Task<IResult> RefreshToken(ISender sender, [FromBody] string token)
        {
            var response = await sender.Send(new RefreshTokenCommand(token));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}

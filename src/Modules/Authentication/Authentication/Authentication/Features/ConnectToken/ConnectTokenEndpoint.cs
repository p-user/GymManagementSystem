namespace Authentication.Authentication.Features.ConnectToken
{
    public class ConnectTokenEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/authentication/connect-token", ConnectToken)
                .WithDescription("Get Token endpoint")
                .WithName("GetToken")
                .WithTags("Authentication")
                .Produces<Results<RequestTokenResponseDto>>();
        }

        private async Task<IResult> ConnectToken(ISender sender, [FromBody] RequestTokenDto requestTokenDto)
        {
            Shared.Results.Results<RequestTokenResponseDto> response = await sender.Send(new ConnectTokenRequest(requestTokenDto));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}

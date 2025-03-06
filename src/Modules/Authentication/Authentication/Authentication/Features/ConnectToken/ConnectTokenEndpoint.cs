

using Authentication.Authentication.Dtos;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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
                .Produces<ConnectTokenResponse>();
        }

        private async Task<IResult> ConnectToken(ISender sender,[FromBody] RequestTokenDto requestTokenDto)
        {
            var response = await sender.Send(new ConnectTokenRequest(requestTokenDto));
            return Results.Ok(response);
        }
    }
}

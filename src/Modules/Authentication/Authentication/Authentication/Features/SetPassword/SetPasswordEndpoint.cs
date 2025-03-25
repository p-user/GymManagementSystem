
namespace Authentication.Authentication.Features.SetPassword
{
    public class SetPasswordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/set-password", SetPassword)
                .WithDescription("Activate your account by setting your password here")
                .WithTags("Authentication")
                .WithSummary("Activate your account by setting your password here . The dto you should provide should be provided from the clientApp")
                ;
        }

        private async Task<IResult> SetPassword(ISender sender, [FromBody] SetPasswordDto dto)
        {
            var setPasswordCommand = new SetPasswordCommand(dto);
            var response = await sender.Send(setPasswordCommand);
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}

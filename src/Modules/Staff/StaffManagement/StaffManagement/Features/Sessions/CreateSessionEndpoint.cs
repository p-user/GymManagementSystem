


namespace StaffManagement.StaffManagement.Features.Sessions
{
    public class CreateSessionEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/staff/session", CreateSession)
                .WithDescription("Creates a new session for the system")
                .WithName("CreateSession")
                .WithTags("Staff")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new session");
        }

        private async Task<IResult> CreateSession([FromBody] CreateSessionDto dto, ISender sender)
        {
            var command = new CreateSessionCommand(dto);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

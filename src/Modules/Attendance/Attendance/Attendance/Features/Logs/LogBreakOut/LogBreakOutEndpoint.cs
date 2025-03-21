
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Attendance.Attendance.Features.Logs.LogBreakOut
{
    public class LogBreakOutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/attendance/break-out", BreakOut)
                .WithDescription("Logs a break out event for an access card")
                .WithName("LogBreakOut")
                .WithTags("Attendance")
                .Produces<Shared.Results.Results>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Log break out event");
        }

        private async Task<IResult> BreakOut([FromBody] Guid accesscardId, ISender sender)
        {
            var command = new LogBreakOutCommand(accesscardId);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

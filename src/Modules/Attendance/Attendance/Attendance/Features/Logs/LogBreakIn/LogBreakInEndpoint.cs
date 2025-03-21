
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Attendance.Attendance.Features.Logs.LogBreakIn
{
    public class LogBreakInEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/attendance/break-in", LogBreakIn)
                .WithDescription("Logs a break-in event for the system")
                .WithName("LogBreakIn")
                .WithTags("Attendance")
                .Produces<Shared.Results.Results>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Log a break-in");
        }

        private async Task<IResult> LogBreakIn([FromBody] Guid accesscardId, ISender sender)
        {
            var command = new LogBreakInCommand(accesscardId);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

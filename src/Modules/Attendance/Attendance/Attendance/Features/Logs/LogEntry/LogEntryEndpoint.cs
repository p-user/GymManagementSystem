using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Attendance.Attendance.Features.Logs.LogEntry
{
    public class LogEntryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/attendance/log-entry", LogEntry)
                .WithDescription("Creates a new entry for the user")
                .WithName("LogEntry")
                .WithTags("Attendance")
                .Produces<Shared.Results.Results>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new entry");
        }

        private async Task<IResult> LogEntry([FromBody] LogDto dto, ISender sender)
        {
            Shared.Results.Results response = await sender.Send(new LogEntryCommand(dto));
            return response.Match(()=> Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

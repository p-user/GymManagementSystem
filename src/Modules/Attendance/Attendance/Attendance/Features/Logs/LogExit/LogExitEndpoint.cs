using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Attendance.Attendance.Features.Logs.LogExit
{
    public class LogExitEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/attendance/log-exit", LogExit)
                .WithDescription("Creates a new exit for the user")
                .WithName("LogExit")
                .WithTags("Attendance")
                .Produces<Shared.Results.Results>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new exit");
        }

        private async Task<IResult> LogExit([FromBody] LogDto dto, ISender sender)
        {
            Shared.Results.Results response = await sender.Send(new LogExitCommand(dto));
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

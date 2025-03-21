
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Attendance.Attendance.Features.AccessCards.DeActivateAccessCard
{
    public class DeActivateAccessCardEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPost("/attendance/access-cards/{accessCardId}/deactivate", Deactivatecard)
                .WithDescription("Deactivates  an  access card for the system")
                .WithName("DeactivateAccessCard")
                .WithTags("AccessCard")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Deactivate access card");
        }

        private async Task<IResult> Deactivatecard([FromRoute]Guid accessCardId,ISender sender)
        {
            var command = new DeActivateAccessCardCommand(accessCardId);
            Shared.Results.Results response =await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

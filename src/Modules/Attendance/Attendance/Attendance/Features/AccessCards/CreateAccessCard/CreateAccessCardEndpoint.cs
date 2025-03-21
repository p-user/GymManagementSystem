
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Attendance.Attendance.Features.AccessCards.CreateAccessCard
{
    public class CreateAccessCardEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/attendance/AccessCards", CreateAccessCard)
                .WithDescription("Creates a new access card for the system")
                .WithName("CreateAccessCard")
                .WithTags("AccessCard")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new access card");
        }
        private async Task<IResult> CreateAccessCard(ISender sender, [FromBody] AccessCardDto dto)
        {
            Shared.Results.Results response = await sender.Send(new CreateAccessCardCommand(dto));
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
   
}

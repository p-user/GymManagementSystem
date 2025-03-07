
using StaffManagement.Contracts.StaffManagement.Dtos;

namespace StaffManagement.StaffManagement.Features.Trainer.CreateTrainer
{
    public class CreateTrainerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/staff/trainer", CreateTrainer)
                .WithDescription("Creates a new trainer for the system")
                .WithName("CreateTrainer")
                .WithTags("Staff")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new trainer"); 
        }


        private async Task<IResult> CreateTrainer(ISender sender , [FromBody] CreateStaffDto dto, CancellationToken ct)
        {
            var command = new CreateTrainerCommand(dto);
            var response = await sender.Send(command, ct);
            return Results.Created($"/staff/trainer", response);
        }
    }
}

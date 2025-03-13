

namespace Membership.Membership.Features.MembershipPlan.CreateMembershipPlan
{
    public class CreateMembershipPlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/membership/membership-plan", CreateMembership)
            .Produces<CreateMembershipPlanResponse>(201)
            .Produces<CreateMembershipPlanResponse>(400)
            .WithSummary("Create a new membership plan")
            .WithTags("Membership")
            .WithDescription("Create a new membership plan");

        }

        private async Task<IResult> CreateMembership(ISender sender, [FromBody] CreateMembershipPlanDto dto)
        {
            var command = new CreateMembershipPlanCommand(dto);
            var response = await sender.Send(command);
            return Results.Created($"/membership/membership-plan/{response.Id}", response);
        }
    }
}

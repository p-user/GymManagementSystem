
namespace Membership.Membership.Features.MembershipPlan.UpdateMembershipPlan
{
    public class UpdateMembershipPlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("membership/membership-plan/{id}", UpdateMembershipPlan)
            .Produces<Guid>(201)
            .Produces<NotFoundResult>(400)
            .WithSummary("Update a new membership plan")
            .WithTags("Membership")
            .WithDescription("Update an existing membership plan"); ;

        }


        private async Task<IResult> UpdateMembershipPlan(ISender sender, [FromRoute] Guid id, [FromBody] CreateMembershipPlanDto dto)
        {
           var command = new UpdateMembershipPlanCommand(dto, id);
           var response = await sender.Send(command);
           return Results.Ok(response);
        }
    }
}

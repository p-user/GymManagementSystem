
namespace Membership.Membership.Features.MembershipPlan.DeleteMembershipPlan
{
    public class DeleteMembershipPlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("membership/membership-plan/{id}", DeleteMembershipPlan)
                .Produces<NoContentResult>(204)
                .Produces<NotFoundResult>(404)
                .WithSummary("Delete a membership plan by id")
                .WithTags("Membership")
                .WithDescription("Delete a membership plan by id");
        }

        private async Task<IResult> DeleteMembershipPlan([FromRoute]Guid id, ISender sender)
        {
            var command = new DeleteMembershipPlanCommand(id);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

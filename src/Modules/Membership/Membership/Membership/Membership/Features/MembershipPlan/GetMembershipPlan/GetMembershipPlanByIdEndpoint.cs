
namespace Membership.Membership.Features.MembershipPlan.GetMembershipPlan
{
    public class GetMembershipPlanByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/membership/membership-plan/{id:guid}", GetMembershipPlan)
                .Produces<GetMembershipPlanByIdResponse>(200)
                .Produces<GetMembershipPlanByIdResponse>(404)
                .WithSummary("Get a membership plan by id")
                .WithTags("Membership")
                .WithDescription("Get a membership plan by id");
        }

        private async Task<IResult> GetMembershipPlan(ISender sender, [FromRoute]Guid id)
        {
            var query = new GetMembershipPlanByIdQuery(id);
            var response = await sender.Send(query);
            return response is not null ? Results.Ok(response) : Results.NotFound();
        }
    }
}


namespace Membership.Membership.Features.MembershipPlan.GetMembershipPlan
{
    public class GetMembershipPlanByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/membership/membership-plan/{id:guid}", GetMembershipPlan)
                .Produces<Results<ViewMembershipPlanDto>>(200)
                .WithSummary("Get a membership plan by id")
                .WithTags("Membership")
                .WithDescription("Get a membership plan by id");
        }

        private async Task<IResult> GetMembershipPlan(ISender sender, [FromRoute]Guid id)
        {
            var query = new GetMembershipPlanByIdQuery(id);
            Results<ViewMembershipPlanDto> response = await sender.Send(query);
            return response.Match(Microsoft.AspNetCore.Http.Results.Ok, ApiResults.Problem);
        }
    }
}

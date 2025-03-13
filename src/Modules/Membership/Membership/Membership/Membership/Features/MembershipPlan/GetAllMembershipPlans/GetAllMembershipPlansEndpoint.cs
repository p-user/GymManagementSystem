using Membership.Membership.Features.MembershipPlan.GetMembershipPlan;

namespace Membership.Membership.Features.MembershipPlan.GetAllMembershipPlans
{
    public class GetAllMembershipPlansEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/membership-plans", GetAllMembershipPlans)
                .Produces<List<GetMembershipPlanByIdResponse>>(200)
                .WithDescription("Get all available membership plans")
                .WithSummary("Get all available membership plans")
                .WithTags("Membership");
        }


        private async Task<IResult> GetAllMembershipPlans(ISender sender)
        {
            var plans = await sender.Send(new GetAllMembershipPlansQuery());
            return Results.Ok(plans);
        }
    }
}

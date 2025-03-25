namespace Membership.Membership.Features.MembershipPlan.GetAllMembershipPlans
{
    public class GetAllMembershipPlansEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/membership-plans", GetAllMembershipPlans)
                .Produces<List<Results<ViewMembershipPlanDto>>>(200)
                .WithDescription("Get all available membership plans")
                .WithSummary("Get all available membership plans")
                .WithTags("Membership");
        }


        private async Task<IResult> GetAllMembershipPlans(ISender sender)
        {
            Results<List<ViewMembershipPlanDto>> plans = await sender.Send(new GetAllMembershipPlansQuery());
            return plans.Match(Microsoft.AspNetCore.Http.Results.Ok, ApiResults.Problem);
        }
    }
}

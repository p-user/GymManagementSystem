



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
            Results<Guid> response = await sender.Send(command);
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}

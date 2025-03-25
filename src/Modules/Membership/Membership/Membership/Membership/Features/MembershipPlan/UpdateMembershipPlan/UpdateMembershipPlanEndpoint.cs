namespace Membership.Membership.Features.MembershipPlan.UpdateMembershipPlan
{
    public class UpdateMembershipPlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("membership/membership-plan/{id}", UpdateMembershipPlan)
            .Produces<Shared.Results.Results>(201)
            .Produces<NotFoundResult>(400)
            .WithSummary("Update a new membership plan")
            .WithTags("Membership")
            .WithDescription("Update an existing membership plan"); ;

        }


        private async Task<IResult> UpdateMembershipPlan(ISender sender, [FromRoute] Guid id, [FromBody] CreateMembershipPlanDto dto)
        {
            var command = new UpdateMembershipPlanCommand(dto, id);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

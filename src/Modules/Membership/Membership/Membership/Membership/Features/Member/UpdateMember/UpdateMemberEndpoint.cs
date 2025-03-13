
namespace Membership.Membership.Features.Member.UpdateMember
{
    public class UpdateMemberEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("membership/GymMembers/{id}", UpdateMember)
                .Produces<UpdateMemberResponse>(200)
                .Produces<UpdateMemberResponse>(404)
                .WithSummary("Update a member by id")
                .WithTags("Membership")
                .WithDescription("Update a member by id");
        }


        private async Task<IResult> UpdateMember(ISender sender, [FromRoute] Guid id, [FromBody] UpdateMemberDto request)
        {
            var command = new UpdateMemberCommand(id, request);
            var response = await sender.Send(command);
            return response is not null ? Results.Ok(response) : Results.NotFound();
        }
    }
}

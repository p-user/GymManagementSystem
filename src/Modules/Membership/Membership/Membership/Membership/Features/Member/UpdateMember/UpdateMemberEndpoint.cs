
namespace Membership.Membership.Features.Member.UpdateMember
{
    public class UpdateMemberEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("membership/GymMembers/{id}", UpdateMember)
                .Produces<Shared.Results.Results>(200)
                .WithSummary("Update a member by id")
                .WithTags("Member")
                .WithDescription("Update a member by id");
        }


        private async Task<IResult> UpdateMember(ISender sender, [FromRoute] Guid id, [FromBody] UpdateMemberDto request)
        {
            var command = new UpdateMemberCommand(id, request);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

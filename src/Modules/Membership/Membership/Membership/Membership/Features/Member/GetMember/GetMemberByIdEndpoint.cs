

namespace Membership.Membership.Features.Member.GetMember
{
    public class GetMemberByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/membership/GymMembers/{id}", GetMemberById)
                .Produces<Results<MemberDto>>(200)
                .WithSummary("Get a member by id")
                .WithTags("Member")
                .WithDescription("Get a member by id");
        }

        private async Task<IResult> GetMemberById(ISender sender,[FromRoute] Guid id)
        {
            var query = new GetMemberByIdQuery(id);
            Results<MemberDto> response = await sender.Send(query);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

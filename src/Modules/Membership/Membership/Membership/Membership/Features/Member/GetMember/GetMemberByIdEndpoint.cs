

namespace Membership.Membership.Features.Member.GetMember
{
    public class GetMemberByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/membership/GymMembers/{id}", GetMemberById)
                .Produces<GetMemberByIdResponse>(200)
                .Produces<GetMemberByIdResponse>(404)
                .WithSummary("Get a member by id")
                .WithTags("Membership")
                .WithDescription("Get a member by id");
        }

        private async Task<IResult> GetMemberById(ISender sender,[FromRoute] Guid id)
        {
            var query = new GetMemberByIdQuery(id);
            var response = await sender.Send(query);
            return response is not null ? Results.Ok(response) : Results.NotFound();
        }
    }
}

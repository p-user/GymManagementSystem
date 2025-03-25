
namespace Membership.Membership.Features.Membership.CreateMembership
{
    public class CreateMembershipEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("membership/membership", CreateMembership)
                .Produces<Shared.Results.Results>(201)
                .WithSummary("Create a membership")
                .WithTags("Membership")
                .WithDescription("Create a membership")
                .RequireAuthorization();
        }

        private async Task<IResult> CreateMembership(HttpContext context, [FromBody] MembershipDto membershipDto, ISender sender)
        {
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            var command = new CreateMembershipCommand(membershipDto, userId);
            Shared.Results.Results results = await sender.Send(command);
            return results.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);

        }
    }
}

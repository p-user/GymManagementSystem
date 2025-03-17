using Microsoft.AspNetCore.Http;

namespace Membership.Membership.Features.Discount.DeactivateDiscount
{
    public class DeactivateDiscountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPut("membership/discount/{id}", DeactivateDiscount)
               .Produces<Shared.Results.Results>(200)
               .WithSummary("Deactivate a discount by id")
               .WithTags("Membership")
               .WithDescription("Deactivate a discount by id");
        }

        private async Task<IResult> DeactivateDiscount([FromRoute]Guid id, ISender sender)
        {
            var command = new DeactivateDiscountCommand(id);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

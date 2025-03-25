

namespace Membership.Membership.Features.Discount.ActivateDiscount
{
    public class ActivateDiscountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("membership/discount/{id:guid}/activate", ActivateDiscount)
                .Produces<Shared.Results.Results>(200)
                .WithSummary("Activate a discount")
                .WithTags("Discount")
                .WithDescription("Activate a discount");
        }
        private async Task<IResult> ActivateDiscount([FromRoute] Guid id, ISender sender)
        {
            var command = new ActivateDiscountCommand(id);
            Shared.Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }

}

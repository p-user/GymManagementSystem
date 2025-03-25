
using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Discount.ApplyDiscount
{
    public class ApplyDiscountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("membership/membershipPlan/{planId:guid}/discount/apply", ApplyDiscount)
                .Produces<Results>(200)
                .WithSummary("Apply a discount to a membership plan price")
                .WithTags("Discount")
                .WithDescription("Get the discounted price of a membership plan");
        }

        private async Task<IResult> ApplyDiscount([FromRoute] Guid planId, [FromBody] ApplyDiscountDto dto, ISender sender)
        {
            var command = new ApplyDiscountCommand(dto.DiscountId, dto.Price, planId);
            Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);

        }
    }
}

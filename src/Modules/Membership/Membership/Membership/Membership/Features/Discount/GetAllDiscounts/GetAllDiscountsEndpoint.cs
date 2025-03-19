


namespace Membership.Membership.Features.Discount.GetAllDiscounts
{
    public class GetAllDiscountsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("membership/discount", GetAllDiscounts)
                .Produces<Results<List<DiscountDto>>>(200)
                .WithSummary("Get all discounts")
                .WithTags("Discount")
                .WithDescription("Lists all discounts");
        }

        private async Task<IResult> GetAllDiscounts(ISender sender)
        {
            var query = new GetAllDiscountsQuery();
            Results<List<DiscountDto>> response = await sender.Send(query);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}

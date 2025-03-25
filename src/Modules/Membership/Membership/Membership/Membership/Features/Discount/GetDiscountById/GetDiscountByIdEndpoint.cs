


namespace Membership.Membership.Features.Discount.GetDiscountById
{
    public class GetDiscountByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("membership/discount/{id:guid}", GetDiscountById)
                .Produces<Results<DiscountDto>>(200)
                .WithSummary("Get a discount by id")
                .WithTags("Discount")
                .WithDescription("Get a discount by id");
        }

        private async Task<IResult> GetDiscountById(ISender sender, [FromRoute] Guid id)
        {
            var query = new GetDiscountByIdQuery(id);
            Results<DiscountDto> response = await sender.Send(query);
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}

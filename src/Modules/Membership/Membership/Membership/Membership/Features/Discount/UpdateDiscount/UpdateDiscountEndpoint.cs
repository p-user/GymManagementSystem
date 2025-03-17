

using Results = Shared.Results;

namespace Membership.Membership.Features.Discount.UpdateDiscount
{
    public class UpdateDiscountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/membership/discount/{id:guid}", UpdateDiscount)
                .Produces<Results.Results>(200)
                .Produces(404)
                .Produces(400)
                .WithSummary("Update a discount")
                .WithTags("Discount")
                .WithDescription("Update a discount");
        }
        private async Task<IResult> UpdateDiscount(ISender sender, [FromRoute] Guid id, [FromBody] UpdateDiscountDto dto)
        {
            var command = new UpdateDiscountCommand(id, dto);
            Results.Results response = await sender.Send(command);
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
    
}

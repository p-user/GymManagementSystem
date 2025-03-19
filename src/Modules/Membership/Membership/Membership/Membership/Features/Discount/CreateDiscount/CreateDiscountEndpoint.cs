
namespace Membership.Membership.Features.Discount.CreateDiscount
{
    public class CreateDiscountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/membership/Discounts", CreateDiscount)
                .Produces<Results<Guid>>(201)
                .Produces(400)
                .WithSummary("Create a discount")
                .WithTags("Discount")
                .WithDescription("Create a discount");
        }
        private async Task<IResult> CreateDiscount([FromBody]CreateDiscountDto request, ISender sender)
        {
            var command = new CreateDiscountCommand(request);
            Results<Guid> response = await sender.Send(command);
            return response.Match(Microsoft.AspNetCore.Http.Results.Ok, ApiResults.Problem);
        }
    }
    
}

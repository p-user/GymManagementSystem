
namespace Membership.Membership.Features.Discount.CreateDiscount
{
    public class CreateDiscountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/membership/Discounts", CreateDiscount)
                .Produces<CreateDiscountResponse>(201)
                .Produces(400)
                .WithSummary("Create a discount")
                .WithTags("Membership")
                .WithDescription("Create a discount");
        }
        private async Task<IResult> CreateDiscount([FromBody]CreateDiscountDto request, ISender sender)
        {
            var command = new CreateDiscountCommand(request);
            var response = await sender.Send(command);
            return Results.Created($"/membership/Discounts/{response.Id}", response);
        }
    }
    
}

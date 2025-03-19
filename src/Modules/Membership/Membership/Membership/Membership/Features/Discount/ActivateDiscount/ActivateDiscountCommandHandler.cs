

using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Discount.ActivateDiscount
{

    public record ActivateDiscountCommand(Guid id) : IRequest<Results>;
    public class ActivateDiscountCommandHandler(MembershipDbContext _context) : IRequestHandler<ActivateDiscountCommand, Results>
    {
        public async Task<Results> Handle(ActivateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.FindAsync(request.id);
            if (discount == null)
            {
                return Results.Failure(MembershipModuleErrors.DicountErrors.NotFound(request.id.ToString()));
            }
            discount.Activate();
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();

        }
    }
}


using Membership.Models;
using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Discount.ApplyDiscount
{
    public record ApplyDiscountCommand(Guid DiscountId, decimal OriginalPrice, Guid MembershipPlanId) : IRequest<Results>;
    public class ApplyDiscountCommandHandler(MembershipDbContext _context) : IRequestHandler<ApplyDiscountCommand, Results>
    {
        public async Task<Results> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.Include(s=>s.ApplicablePlans).FirstOrDefaultAsync(s=>s.Id==request.DiscountId, cancellationToken);
            if (discount == null)
            {
                return Results.Failure(MembershipModuleErrors.DicountErrors.NotFound(request.DiscountId.ToString()));
            }

            if (!discount.IsActive)
            {
                return Results.Failure(MembershipModuleErrors.DicountErrors.NotActiveProblem(request.DiscountId.ToString()));
            }

            if (!discount.IsApplicableToPlan(request.MembershipPlanId))
            {
                return Results.Failure(MembershipModuleErrors.DicountErrors.MembershipPlanProblem(request.MembershipPlanId.ToString()));
            }

            var discountedPrice = discount.ApplyDiscount(request.OriginalPrice);


            return Results<decimal>.Success(discountedPrice);
        }
    }
}

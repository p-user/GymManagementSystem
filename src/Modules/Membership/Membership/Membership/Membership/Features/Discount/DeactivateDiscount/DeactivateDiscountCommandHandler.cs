

using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Discount.DeactivateDiscount
{
    public record DeactivateDiscountCommand (Guid Id) : IRequest<Results>;
    public class DeactivateDiscountCommandHandler(MembershipDbContext membershipDbContext) : IRequestHandler<DeactivateDiscountCommand, Results>
    {
        public async Task<Results> Handle(DeactivateDiscountCommand request, CancellationToken cancellationToken)
        {
           var entity = await membershipDbContext.Discounts.FindAsync(request.Id);
            if (entity == null)
            {
                return Results.Failure(MembershipModuleErrors.DicountErrors.NotFound(request.Id.ToString()));
            }

            entity.Deactivate();
            membershipDbContext.Discounts.Update(entity);
            await membershipDbContext.SaveChangesAsync(cancellationToken);

            return Results.Success();
        }
    }
}

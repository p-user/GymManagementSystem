

namespace Membership.Membership.Features.Discount.CreateDiscount
{
    public record CreateDiscountCommand(CreateDiscountDto dto) : IRequest<Results<Guid>>;

    public class CreateDiscountCommandHandler(MembershipDbContext _context, CreateDiscountCommandValidator _validator) : IRequestHandler<CreateDiscountCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Errors.Select(s => s.ErrorMessage).FirstOrDefault());
            }
            var discount = CreateDiscount(request.dto);
            var entity = await _context.Discounts.AddAsync(discount);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity.Id;


        }

        private Models.Discount CreateDiscount(CreateDiscountDto dto)
        {
            return Models.Discount.Create(dto.Code, dto.Description, dto.DiscountPercentage, dto.DiscountAmount, dto.StartDate, dto.EndDate, dto.UsageLimit, dto.AppliesToAllPlans);
        }
    }
}

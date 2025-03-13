

namespace Membership.Membership.Features.Discount.CreateDiscount
{
    public record CreateDiscountCommand(CreateDiscountDto dto) : IRequest<CreateDiscountResponse>;
    public record CreateDiscountResponse(Guid Id);  

    public class CreateDiscountCommandHandler(MembershipDbContext _context, CreateDiscountCommandValidator _validator) : IRequestHandler<CreateDiscountCommand, CreateDiscountResponse>
    {
        public async  Task<CreateDiscountResponse> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Errors.Select(s=>s.ErrorMessage).FirstOrDefault());
            }
            var discount= CreateDiscount(request.dto);
            var entity = await _context.Discounts.AddAsync(discount);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateDiscountResponse(entity.Entity.Id);


        }

        private Models.Discount CreateDiscount(CreateDiscountDto dto)
        {
            return Models.Discount.Create(dto.Code, dto.Description, dto.DiscountPercentage, dto.DiscountAmount, dto.StartDate, dto.EndDate, dto.UsageLimit, dto.AppliesToAllPlans);
        }
    }
}

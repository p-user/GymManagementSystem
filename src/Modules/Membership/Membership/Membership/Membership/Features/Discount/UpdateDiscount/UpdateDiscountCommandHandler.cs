

using System.ComponentModel.DataAnnotations;

namespace Membership.Membership.Features.Discount.UpdateDiscount
{
    public record UpdateDiscountCommand(Guid Id, UpdateDiscountDto dto) : IRequest<UpdateDiscountResponse>;
    public record UpdateDiscountResponse(Guid Id);
    public class UpdateDiscountCommandHandler(MembershipDbContext _context, UpdateDiscountCommandValidator _validator) : IRequestHandler<UpdateDiscountCommand, UpdateDiscountResponse>
    {
        public async Task<UpdateDiscountResponse> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.FindAsync(request.Id);
            if (discount == null)
            {
                throw new Exception("Discount was not found!");
            }
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString());
            }
            UpdateDiscount(discount, request.dto);
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateDiscountResponse(discount.Id);
        }
        private void UpdateDiscount(Models.Discount discount, UpdateDiscountDto dto)
        {
            discount.Update(dto.Code, dto.Description, dto.DiscountPercentage, dto.DiscountAmount, dto.StartDate, dto.EndDate, dto.UsageLimit, dto.UsageCount, dto.AppliesToAllPlans);
        }
    } 
    
}

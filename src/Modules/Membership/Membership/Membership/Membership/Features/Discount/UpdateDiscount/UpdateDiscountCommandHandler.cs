
using Results= Shared.Results.Results;
using Membership.Membership.ModuleErrors;
using Shared.Exceptions;

namespace Membership.Membership.Features.Discount.UpdateDiscount
{
    public record UpdateDiscountCommand(Guid Id, UpdateDiscountDto dto) : IRequest<Results>;
    public class UpdateDiscountCommandHandler(MembershipDbContext _context, UpdateDiscountCommandValidator _validator) : IRequestHandler<UpdateDiscountCommand,Results>
    {
        public async Task<Results> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.FindAsync(request.Id);
            if (discount == null)
            {
                return Results.Failure(DicountErrors.NotFound(request.Id.ToString()));
            }
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
               .Select(e => new Error("Validation.Failed", e.ErrorMessage, ErrorType.Validation))
               .ToArray();

                return Results.Failure(new ValidationError(errors));

            }
            UpdateDiscount(discount, request.dto);
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
        private void UpdateDiscount(Models.Discount discount, UpdateDiscountDto dto)
        {
            discount.Update(dto.Code, dto.Description, dto.DiscountPercentage, dto.DiscountAmount, dto.StartDate, dto.EndDate, dto.UsageLimit, dto.UsageCount, dto.AppliesToAllPlans);
        }
    } 
    
}

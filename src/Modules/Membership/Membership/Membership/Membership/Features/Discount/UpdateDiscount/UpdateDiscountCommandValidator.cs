
using FluentValidation;

namespace Membership.Membership.Features.Discount.UpdateDiscount
{
    public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.dto).NotNull();
            RuleFor(x => x.dto.Code).NotEmpty();
            RuleFor(x => x.dto.Description).NotEmpty();
            RuleFor(x => x.dto.DiscountPercentage).NotEmpty();
            RuleFor(x => x.dto.DiscountAmount).NotEmpty();
            RuleFor(x => x.dto.StartDate).NotEmpty();
            RuleFor(x => x.dto.EndDate).NotEmpty();
            RuleFor(x => x.dto.UsageLimit).NotEmpty();
            RuleFor(x => x.dto.UsageCount).NotEmpty();
            RuleFor(x => x.dto.AppliesToAllPlans).NotEmpty();

            RuleFor(x => x)
              .Must(x => (x.dto.DiscountPercentage.HasValue && !x.dto.DiscountAmount.HasValue) ||
                         (!x.dto.DiscountPercentage.HasValue && x.dto.DiscountAmount.HasValue))
              .WithMessage("Either DiscountPercentage or DiscountAmount must be provided, but not both.");

            RuleFor(x => x.dto.StartDate)
               .LessThanOrEqualTo(x => x.dto.EndDate)
               .When(x => x.dto.EndDate.HasValue)
               .WithMessage("StartDate must be before or equal to EndDate.");
        }
    }
   
}

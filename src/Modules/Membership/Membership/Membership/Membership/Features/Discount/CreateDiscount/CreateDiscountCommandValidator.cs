using FluentValidation;

namespace Membership.Membership.Features.Discount.CreateDiscount
{
    public class CreateDiscountCommandValidator: AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            // Validate that either DiscountPercentage or DiscountAmount is not null, but not both.
            RuleFor(x => x)
                .Must(x => (x.dto.DiscountPercentage.HasValue && !x.dto.DiscountAmount.HasValue) ||
                           (!x.dto.DiscountPercentage.HasValue && x.dto.DiscountAmount.HasValue))
                .WithMessage("Either DiscountPercentage or DiscountAmount must be provided, but not both.");

            RuleFor(x => x.dto.Code)
                .NotEmpty().WithMessage("Code is required.");

            RuleFor(x => x.dto.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.dto.StartDate)
                .LessThanOrEqualTo(x => x.dto.EndDate)
                .When(x => x.dto.EndDate.HasValue)
                .WithMessage("StartDate must be before or equal to EndDate.");

        }
    }
}

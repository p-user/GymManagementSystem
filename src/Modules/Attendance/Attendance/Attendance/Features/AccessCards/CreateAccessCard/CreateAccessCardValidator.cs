

using FluentValidation;

namespace Attendance.Attendance.Features.AccessCards.CreateAccessCard
{
    public class CreateAccessCardValidator : AbstractValidator<CreateAccessCardCommand>
    {
        public CreateAccessCardValidator()
        {
            RuleFor(c => c.dto.OwnerId).NotEqual(Guid.Empty).WithMessage("OwnerId must not be an empty GUID.");
            RuleFor(c => c.dto.OwnerType).IsInEnum().WithMessage("Invalid OwnerType.");
        }
    }
}

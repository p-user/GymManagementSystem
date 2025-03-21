using FluentValidation;
using Shared.Exceptions;

namespace Attendance.Attendance.Features.AccessCards.CreateAccessCard
{
    public record CreateAccessCardCommand(AccessCardDto dto) : IRequest<Results>;

    public class CreateAccessCardCommandHandler(AttendanceDbContext _context, IValidator<CreateAccessCardCommand> _validator) : IRequestHandler<CreateAccessCardCommand, Results>
    {
        public async  Task<Results> Handle(CreateAccessCardCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var validationFailures = validationResult.Errors.Select(failure => new Error(null, failure.ErrorMessage, ErrorType.Validation));
                return Results.Failure(validationFailures.FirstOrDefault());
            }
            var accessCard = Create(request.dto);
            await _context.AccessCards.AddAsync(accessCard);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }

        private AccessCard Create(AccessCardDto dto)
        {
            return AccessCard.Create(dto.OwnerId, dto.OwnerType);
        }
    }
}

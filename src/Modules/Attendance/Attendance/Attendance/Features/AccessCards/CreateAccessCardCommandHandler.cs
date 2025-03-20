

namespace Attendance.Attendance.Features.AccessCards
{
    public record CreateAccessCardCommand(AccessCardDto dto) : IRequest<Results>;

    public class CreateAccessCardCommandHandler(AttendanceDbContext _context) : IRequestHandler<CreateAccessCardCommand, Results>
    {
        public async  Task<Results> Handle(CreateAccessCardCommand request, CancellationToken cancellationToken)
        {
            // Todo: Add validation for user and card number
            var accessCard = Create(request.dto);
            await _context.AccessCards.AddAsync(accessCard);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }

        private AccessCard Create(AccessCardDto dto)
        {
            return AccessCard.Create(dto.UserId, dto.CardNumber);
        }
    }
}

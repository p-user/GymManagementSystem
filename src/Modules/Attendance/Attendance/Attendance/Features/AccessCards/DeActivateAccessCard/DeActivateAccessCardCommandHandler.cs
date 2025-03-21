
namespace Attendance.Attendance.Features.AccessCards.DeActivateAccessCard
{
    public record DeActivateAccessCardCommand(Guid AccessCardId) : IRequest<Results>;
    public class DeActivateAccessCardCommandHandler(AttendanceDbContext _context) : IRequestHandler<DeActivateAccessCardCommand, Results>
    {
        public async Task<Results> Handle(DeActivateAccessCardCommand request, CancellationToken cancellationToken)
        {
            var accessCard = await _context.AccessCards.FindAsync(request.AccessCardId);
            if (accessCard is null)
            {
                return Results.Failure(AccessCardErrors.NotFound(request.AccessCardId));
            }

            accessCard.Deactivate();
            _context.Update(accessCard);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
    }
}

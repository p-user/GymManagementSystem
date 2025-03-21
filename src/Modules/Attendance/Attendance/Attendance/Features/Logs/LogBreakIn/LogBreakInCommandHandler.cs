

namespace Attendance.Attendance.Features.Logs.LogBreakIn
{

    public record LogBreakInCommand(Guid AccessCardId) : IRequest<Results>;
    public class LogBreakInCommandHandler(AttendanceDbContext _context) : IRequestHandler<LogBreakInCommand, Results>
    {
        public async Task<Results> Handle(LogBreakInCommand request, CancellationToken cancellationToken)
        {
            var accessCard = await _context.AccessCards.FindAsync(request.AccessCardId);
            if (accessCard == null)
            {
                return Results.Failure(AccessCardErrors.NotFound(request.AccessCardId));
            }

            var log = AttendanceLog.CreateBreakIn(accessCard.OwnerId, accessCard.Id);
            await _context.AttendanceLogs.AddAsync(log, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Results.Success(log);


        }
    }
}

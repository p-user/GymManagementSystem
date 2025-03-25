namespace Attendance.Attendance.Features.Logs.LogBreakOut
{
    public record LogBreakOutCommand(Guid AccessCardId) : IRequest<Results>;
    public class LogBreakOutCommandHandler(AttendanceDbContext _context) : IRequestHandler<LogBreakOutCommand, Results>
    {
        public async Task<Results> Handle(LogBreakOutCommand request, CancellationToken cancellationToken)
        {
            var accessCard = await _context.AccessCards.FindAsync(request.AccessCardId);
            if (accessCard == null)
            {
                return Results.Failure(AccessCardErrors.NotFound(request.AccessCardId));
            }

            var log = AttendanceLog.CreateBreakOut(accessCard.OwnerId, accessCard.Id);
            await _context.AttendanceLogs.AddAsync(log, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Results.Success(log);
        }
    }
}

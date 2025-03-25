using Membership.Contracts.Membership.ModuleErrors;

namespace Attendance.Attendance.Features.Logs.LogEntry
{

    public record LogEntryCommand(LogDto dto) : IRequest<Results>;
    public class LogEntryCommandHandler(AttendanceDbContext _context, ISender _sender) : IRequestHandler<LogEntryCommand, Results>
    {
        public async Task<Results> Handle(LogEntryCommand request, CancellationToken cancellationToken)
        {

            #region verify user and access card

            var hasValidMembership = await _sender.Send(new CheckForValidMembershipQuery(request.dto.UserId), cancellationToken);
            if (hasValidMembership.Value is false)
            {
                return Results.Failure(MembershipPlanErrors.NoValidMembershipForUser(request.dto.UserId));
            }

            var accessCard = await _context.AccessCards.FindAsync(request.dto.AccessCardId);
            if (accessCard is null)
            {
                return Results.Failure(AccessCardErrors.NotFound(request.dto.AccessCardId));
            }

            #endregion


            var attendanceLog = AttendanceLog.CreateEntry(request.dto.UserId, accessCard);
            await _context.AddAsync(attendanceLog);
            await _context.SaveChangesAsync(cancellationToken);

            return Results.Success();

        }
    }
}

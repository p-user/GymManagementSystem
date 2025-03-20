using Attendance.Contracts.Attendance.ModuleErrors;
using Membership.Contracts.Membership.ModuleErrors;

namespace Attendance.Attendance.Features.Logs.LogExit
{
    public record LogExitCommand(LogDto dto) : IRequest<Results>;

    public class LogExitCommandHandler(ISender _sender, AttendanceDbContext _context) : IRequestHandler<LogExitCommand, Results>
    {
        public async Task<Results> Handle(LogExitCommand request, CancellationToken cancellationToken)
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


            var attendanceLog = AttendanceLog.CreateExit(request.dto.UserId, request.dto.AccessCardId);
            await _context.AddAsync(attendanceLog);
            await _context.SaveChangesAsync(cancellationToken);

            return Results.Success();

        }
    }
    
}

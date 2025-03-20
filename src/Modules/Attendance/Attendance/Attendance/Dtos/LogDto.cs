

namespace Attendance.Attendance.Dtos
{
    public record  LogDto
    {
        public Guid UserId { get; init; }
        public Guid AccessCardId { get; init; }
    }
}

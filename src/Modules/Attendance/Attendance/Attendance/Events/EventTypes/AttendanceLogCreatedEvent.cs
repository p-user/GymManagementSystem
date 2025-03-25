namespace Attendance.Attendance.Events.EventTypes
{
    public class AttendanceLogCreatedEvent : IDomainEvent
    {
        public Guid UserId { get; set; } = default!;
    }

}

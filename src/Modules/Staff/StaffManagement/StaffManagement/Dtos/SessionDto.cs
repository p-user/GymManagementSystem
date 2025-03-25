
namespace StaffManagement.StaffManagement.Dtos
{
    public record SessionDto
    {

        public Guid TrainerId { get; init; }
        public Guid MemberId { get; init; }
        public DateTime ScheduledAt { get; init; }
        public SessionStatus Status { get; init; }

        public enum SessionStatus
        {
            Scheduled,
            Canceled,
            Completed
        }
    }


    public record CreateSessionDto : SessionDto
    {

    }

    public record UpdateSessionDto : SessionDto
    {
        public Guid Id { get; init; }
    }

    public record ViewSessionDto : SessionDto
    {
        public Guid Id { get; init; }
    }
}

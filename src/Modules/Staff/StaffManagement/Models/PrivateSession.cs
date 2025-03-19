
using Shared.DDD;

namespace StaffManagement.Models
{
    public class PrivateSession : Aggregation<Guid>
    {
        public Guid Id { get; private set; }
        public Guid TrainerId { get; private set; }
        public Guid MemberId { get; private set; }
        public DateTime ScheduledAt { get; private set; }
        public SessionStatus Status { get; private set; }

        private PrivateSession() { }

        public enum SessionStatus
        {
            Scheduled,
            Canceled,
            Completed
        }

        public static PrivateSession Create(Guid trainerId, Guid memberId, DateTime scheduledAt)
        {
            if (scheduledAt < DateTime.UtcNow.AddHours(1))
                throw new InvalidOperationException("Sessions must be scheduled at least an hour in advance.");

            return new PrivateSession()
            {
                Id = Guid.NewGuid(),
                TrainerId = trainerId,
                MemberId = memberId,
                ScheduledAt = scheduledAt,
                Status = SessionStatus.Scheduled
            };
        }

        public void Cancel()
        {
            if (Status == SessionStatus.Completed)
                throw new InvalidOperationException("Completed sessions cannot be canceled.");

            Status = SessionStatus.Canceled;
        }



        public void Complete()
        {
            if (Status != SessionStatus.Scheduled)
                throw new InvalidOperationException("Only scheduled sessions can be marked as completed.");

            Status = SessionStatus.Completed;
        }
    }
}

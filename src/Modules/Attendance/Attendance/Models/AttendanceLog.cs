
namespace Attendance.Models
{
    public class AttendanceLog : Aggregation<Guid>
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AccessCardId { get; private set; }
        public AttendanceType Type { get; private set; }
        public DateTime Timestamp { get; private set; }

        private AttendanceLog()
        {

        }
        public enum AttendanceType
        {
            Entry,
            Exit,
            BreakIn,
            BreakOut
        }

        public static AttendanceLog CreateEntry(Guid userId, AccessCard accessCard)
        {
            var entry = new AttendanceLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AccessCardId = accessCard.Id,
                Type = AttendanceType.Entry,

            };

            if (accessCard.OwnerType == AccessCard.AccessCardOwnerType.Member)
            {
               
                entry.AddDomainEvent(new AttendanceLogCreatedEvent() { UserId = userId});
            }

            return entry;
        }

        public static AttendanceLog CreateExit(Guid userId, Guid accessCardId)
            => new AttendanceLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AccessCardId = accessCardId,
                Type = AttendanceType.Exit,
            };

        public static AttendanceLog CreateBreakIn(Guid userId, Guid accessCardId)
            => new AttendanceLog 
            { Id = Guid.NewGuid(),
                UserId = userId, 
                AccessCardId = accessCardId,
                Type = AttendanceType.BreakIn
            };

        public static AttendanceLog CreateBreakOut(Guid userId, Guid accessCardId)
            => new AttendanceLog 
            { Id = Guid.NewGuid(), 
               UserId = userId, 
                AccessCardId = accessCardId, 
                Type = AttendanceType.BreakOut 
            };
    }
}

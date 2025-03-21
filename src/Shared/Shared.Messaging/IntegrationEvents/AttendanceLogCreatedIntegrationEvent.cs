

namespace Shared.Messaging.IntegrationEvents
{
    public record AttendanceLogCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; } = default!;
    }
}

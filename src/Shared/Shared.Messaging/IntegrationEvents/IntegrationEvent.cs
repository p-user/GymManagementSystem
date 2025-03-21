
namespace Shared.Messaging.IntegrationEvents
{
    public record IntegrationEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime CreatedAt => DateTime.UtcNow;
        public string EventTypeName => GetType().AssemblyQualifiedName;
    }
}


namespace Shared.DDD
{
    public interface IAggregation : IEntity
    {
        IReadOnlyList<IDomainEvent> Events { get; }
        IReadOnlyList<IDomainEvent> ClearEvents();
    }

    public interface IAggregation<T> : IAggregation, IEntity<T>
    {
    }
}

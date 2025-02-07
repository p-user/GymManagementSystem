
namespace Shared.DDD
{
    public abstract class Aggregation<T> : Entity<T>, IAggregation<T>
    {
       private readonly List<IDomainEvent> _domainEvents = new();
       public IReadOnlyList<IDomainEvent> Events => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
           
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyList<IDomainEvent> ClearEvents()
        {
            var events = _domainEvents.ToList();
            _domainEvents.Clear();
            return events;
        }
    }
}

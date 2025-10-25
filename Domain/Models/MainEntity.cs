

using Domain.Events;

namespace Domain.Models
{
    public abstract class MainEntity
    {
        private readonly List<IDomainEvent> _domailEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domailEvents.AsReadOnly();
        protected void AddDomainEvent(IDomainEvent someEvent)
        {
            _domailEvents.Add(someEvent);
        }
    }
}

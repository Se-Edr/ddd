

using Domain.Events;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public abstract class MainEntity
    {
        private readonly List<IDomainEvent> _domailEvents = new();
        [JsonIgnore]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domailEvents.AsReadOnly();
        protected void AddDomainEvent(IDomainEvent someEvent)
        {
            _domailEvents.Add(someEvent);
        }
        public void ClearDomainEvents()
        {
            _domailEvents.Clear();
        }
    }
}

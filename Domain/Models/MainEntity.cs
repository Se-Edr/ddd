
using Domain.Events;
using System.Text.Json.Serialization;

namespace Domain.Models
{

    public abstract class MainEntity
    {
        private readonly List<IDomainEvent> _domailEvents = new();
        [JsonIgnore]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domailEvents.AsReadOnly();
       
        public void ClearDomainEvents()
        {
            _domailEvents.Clear();
        }
        protected void RaiseDomainEvent(IDomainEvent eventToRaise)
        {
            _domailEvents.Add(eventToRaise);
        }
    }
}

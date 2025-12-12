
namespace Domain.Events
{
    public interface IDomainEventHandler<ForEventType> where ForEventType:IDomainEvent
    {
        Task handle(ForEventType someEvent);
    }

}

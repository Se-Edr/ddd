using Microsoft.Extensions.DependencyInjection;

namespace Domain.Events
{
    /// <summary>
    ///gets all registered domainevent handlers
    /// </summary>
    public interface IDomainDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
    }
    public class DomainDispatcher(IServiceProvider serviceProvider) : IDomainDispatcher
    {
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
        {

            foreach (IDomainEvent domainEvent in domainEvents) 
            {
                Type handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handlers= serviceProvider.GetServices(handlerType);

                foreach(var handler in handlers)
                {
                    var method = handlerType.GetMethod("handle");
                    if(method != null)
                    {
                        //concrete domaineventhandler with concrete event
                        // ServiceBasePriceUpdatedHandler.handle(ServiceBasePriceUpdatedEvent ev)
                        await (Task)method.Invoke(handler, new[] { domainEvent })!;
                    }
                }

            }
        }
    }
}

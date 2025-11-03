

using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Domain.Events.Service
{
    public interface IDomainDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
    }

    public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainDispatcher
    {
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            
            //throw new NotImplementedException();

            foreach(var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventhandler<>).MakeGenericType(domainEvent.GetType());
                var handlers = serviceProvider.GetServices(handlerType);

                foreach (var handler in handlers)
                {

                    var method = handlerType.GetMethod("HandleAsync");
                    if (method != null)
                    {
                        await (Task)method.Invoke(handler, new[] { domainEvent })!;
                    }
                }
            }

        }
    }
}

using Domain.Models.Operation;
using Domain.Repositories;

namespace Domain.Events.Service
{
    public record ServiceBasePriceUpdatedEvent(int newBasePrice) : IDomainEvent;
    public class ServiceBasePriceUpdatedHandler(IUnitOfWork _uow,ITrackedEntitiesCollection _trackedEntities) : IDomainEventHandler<ServiceBasePriceUpdatedEvent>
    {
        public async Task handle(ServiceBasePriceUpdatedEvent someEvent)
        {
            List<Procedure> nonfixed = await _uow.procedureRepository.GetNonFixedPrice();

            foreach (Procedure procedure in nonfixed)
            {
                _trackedEntities.AppendEntityToTracker(procedure);
                procedure.RecalculatePrice(someEvent.newBasePrice);
            }
        }
    }

}

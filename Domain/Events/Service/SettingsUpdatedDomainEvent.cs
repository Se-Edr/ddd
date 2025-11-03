using Domain.Models.Operation;
using Domain.Repositories;

namespace Domain.Events.Service
{
    public record SettingsUpdatedDomainEvent(int newBasePrice):IDomainEvent
    {
    }

    public interface IDomainEventhandler<TEvent> where TEvent :IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent);
    }

    public class RecalculateProcedureHandler :IDomainEventhandler<SettingsUpdatedDomainEvent>
    {
        private readonly IUnitOfWork _uow;

        public RecalculateProcedureHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task HandleAsync(SettingsUpdatedDomainEvent domainEvent)
        {
            List<Procedure> preoc = await _uow.procedureRepository.GetNonFixedPrice();

            foreach (Procedure proc in preoc)
            {
               proc.RecalculatePrice(domainEvent.newBasePrice);
            }

            //await _uow.SaveChangesAsync();
        }
    }
}

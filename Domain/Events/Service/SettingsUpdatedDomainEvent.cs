using Domain.Models.Operation;
using Domain.Repositories;

namespace Domain.Events.Service
{
    public record SettingsUpdatedDomainEvent(int newBasePrice):IDomainEvent
    {
    }

    public class RecalculateProcedureHandler
    {
        private readonly IProcedureRepository _repo;

        public RecalculateProcedureHandler(IProcedureRepository repo)
        {
            _repo = repo;
        }

        public async Task HandleAsync()
        {
            List<Procedure> preoc = await _repo.GetNonFixedPrice();

            foreach (Procedure proc in preoc)
            {
               // proc.RecalculatePrice();
            }
        }
    }
}

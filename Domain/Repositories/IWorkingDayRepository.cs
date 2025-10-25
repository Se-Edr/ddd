using Domain.Models.Termin;

namespace Domain.Repositories
{
    public interface IWorkingDayRepository:IRepository<WorkingDay>
    {
        Task<WorkingDay?> GetDayByDate(DateOnly date);
        Task<List<WorkingDay?>> GetDays(DateTime start, DateTime finish);
    }
}

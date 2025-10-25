using Domain.Models.Appointment;
using Domain.Models.Termin;

namespace Domain.Repositories
{
    public interface IMeetingRepository :IRepository<Meeting>
    {
        Task<Meeting> GetByDay(WorkingDay day);

    }
}

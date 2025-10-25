using Domain.Models.Appointment;
using Domain.Models.Termin;
using Domain.Repositories;
namespace Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        public Task AddAsync(Meeting entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Meeting> GetByDay(WorkingDay day)
        {
            throw new NotImplementedException();
        }

        public Task<Meeting?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Meeting entity)
        {
            throw new NotImplementedException();
        }
    }
}

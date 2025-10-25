

using Domain.Models.Termin;
using Domain.Repositories;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WorkingDayRepository(DatabaseContext _context) : IWorkingDayRepository
    {
        public async Task AddAsync(WorkingDay entity)
        {
            await _context.DaysTable.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkingDay?> GetByIdAsync(Guid id)
        {
            WorkingDay day = await _context.DaysTable.FirstOrDefaultAsync(x=>x.DayId.Equals(id));
            return day;
        }

        public async Task<WorkingDay?> GetDayByDate(DateOnly date)
        {
            WorkingDay? day = await _context.DaysTable.FirstOrDefaultAsync(d=>d.Date.Equals(date));
            return day;
        }
        public async Task<List<WorkingDay?>> GetDays(DateTime start,DateTime finish)
        {
            List<WorkingDay> days = await _context.DaysTable.ToListAsync();
            if(days==null || days.Count < 1)
            {
                throw new Exception("Empty list of days");
            }
            return days;
        }

        public async Task UpdateAsync(WorkingDay entity)
        {
           _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

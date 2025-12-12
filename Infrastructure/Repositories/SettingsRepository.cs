using Domain.Events;
using Domain.Models.ServiceSetting;
using Domain.Repositories;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class SettingsRepository(DatabaseContext _context,ITrackedEntitiesCollection _entitiesCollection) : IServiceSettingRepository
    {
        
        public async Task AddAsync(ServiceSettings entity)
        {
            await _context.SettingsTable.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceSettings?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceSettings> GetSettings()
        {
            ServiceSettings sett= await _context.SettingsTable.FirstOrDefaultAsync();
            if (sett == null)
            {
                throw new Exception("No settings");
            }
            return sett;
        }

        public async Task UpdateAsync(ServiceSettings entity)
        {
            _entitiesCollection.AppendEntityToTracker(entity);
            _context.SettingsTable.Update(entity);
        }
    }

    
}

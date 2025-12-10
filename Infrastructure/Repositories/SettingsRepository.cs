using Domain.DTOs;
using Domain.Models.ServiceSetting;
using Domain.Repositories;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class SettingsRepository(DatabaseContext _context) : IServiceSettingRepository
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

        public Task UpdateAsync(ServiceSettings entity)
        {
            throw new NotImplementedException();
        }
    }

    
}

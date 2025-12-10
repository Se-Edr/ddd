using Domain.DTOs;
using Domain.Models.ServiceSetting;
using Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class CachedSettingsRepository(
        IServiceSettingRepository _serviceRepo,
        IDistributedCache _cache) : IServiceSettingRepository
    {
        private readonly string _cacheKey = "service_settings";

        public async Task AddAsync(ServiceSettings entity)
        {
            string settingsJson = JsonSerializer.Serialize(
               new ServiceSettingsDTO(
               entity.BasePricePerWindow,
               entity.BasePricePerWindow,
               entity.Morning.startTime,
               entity.Morning.finishTime,
               entity.WholeDay.startTime,
               entity.WholeDay.finishTime
               ));
            await _cache.SetStringAsync(_cacheKey, settingsJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(40)
            });

            await _serviceRepo.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
           await _serviceRepo.DeleteAsync(id);
        }

        public async Task<ServiceSettings?> GetByIdAsync(Guid id)
        {
            return await _serviceRepo.GetByIdAsync(id);
        }

        public async Task<ServiceSettings> GetSettings()
        {
            string? cachedjson = await _cache.GetStringAsync(_cacheKey);
            if (!string.IsNullOrEmpty(cachedjson))
            {
                ServiceSettingsDTO cachedSettings = JsonSerializer.Deserialize<ServiceSettingsDTO>(cachedjson);

                if (cachedSettings != null)
                {
                    return ServiceSettings.SetServiceSettings(
                        cachedSettings.BaseWindowInMinutes,
                        cachedSettings.BasePricePerWindow,
                        new Shift(cachedSettings.MorningStart, cachedSettings.MorningFinish),
                        new Shift(cachedSettings.WholeDayStart, cachedSettings.WholeDayFinish));

                }
            }

            ServiceSettings sett = await _serviceRepo.GetSettings();

            ServiceSettingsDTO cached = new ServiceSettingsDTO(
                sett.BasePricePerWindow,
                sett.BasePricePerWindow,
                sett.Morning.startTime,
                sett.Morning.finishTime,
                sett.WholeDay.startTime,
                sett.WholeDay.finishTime
                );

            string settingsJson = JsonSerializer.Serialize(cached);
            await _cache.SetStringAsync(_cacheKey, settingsJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(40)
            });

            return sett;

        }

        public async Task UpdateAsync(ServiceSettings entity)
        {
            await _serviceRepo.UpdateAsync(entity);
        }
    }
}



using Domain.Models.ServiceSetting;

namespace Domain.Repositories
{
    public interface IServiceSettingRepository:IRepository<ServiceSettings>
    {
        Task<ServiceSettings> GetSettings();
    }
}

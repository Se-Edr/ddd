using Domain.Models.ServiceSetting;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Settings
{

    public record DsiplayCommandSettingsCommand:IQuery<ServiceSettings>;
    public class DisplaySpecsCommandHandler(IUnitOfWork _uow) : IRequestHandler<DsiplayCommandSettingsCommand, ServiceSettings>
    {
        public async Task<ServiceSettings> Handle(DsiplayCommandSettingsCommand request)
        {
            ServiceSettings settings = await _uow.serviceSettingsRepository.GetSettings();
            return settings;
        }
    }
}

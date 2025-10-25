using Application.CQRS.TestingHandl;
using Domain.Models.ServiceSetting;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Settings
{
    public record CreateServiceCommand(
        int window,
        int price,
        TimeOnly morningStart,
        TimeOnly morningFinish,
        TimeOnly WholeStart,
        TimeOnly WholeFinish
        ) : ICommand<ServiceSettings>;

    public class CreateServiceCommandHandler(IUnitOfWork _uow) : IRequestHandler<CreateServiceCommand, ServiceSettings>
    {
        public async Task<ServiceSettings> Handle(CreateServiceCommand request)
        {
            ServiceSettings existing = await _uow.serviceSettingsRepository.GetSettings();

            Shift morn = new Shift(request.morningStart, request.morningFinish);
            Shift whole = new Shift(request.WholeStart, request.WholeFinish);
           

            if (existing == null)
            {
                ServiceSettings settings = ServiceSettings.SetServiceSettings(
                request.window,
                request.price,
                morn, whole
                );

                await _uow.serviceSettingsRepository.AddAsync(settings);
                await _uow.SaveChangesAsync();

                return settings;
            }
            else
            {
                existing.EditServiceSeTtings(request.window,
                request.price,
                morn, whole,existing);


                await _uow.SaveChangesAsync();
                return existing;
            }
        }
    }
}

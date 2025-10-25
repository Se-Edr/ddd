using Domain.Models.ServiceSetting;
using Domain.Models.Termin;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.WorkingDay
{

    public record EditWorkingDayCommand(Guid dayId,int shift):ICommand;
    public class EditWorkingDayCommandHandler(IUnitOfWork _uow) : IRequestHandler<EditWorkingDayCommand>
    {
        public async Task Handle(EditWorkingDayCommand request)
        {
            Domain.Models.Termin.WorkingDay existingDay = await _uow.workingDayRepository.GetByIdAsync(request.dayId);
            if (existingDay == null)
            {
                throw new Exception("Day not found");
            }
            ServiceSettings settings = await _uow.serviceSettingsRepository.GetSettings();
            Shift newShift = settings.WholeDay;
            string shiftStr = null;

            switch (request.shift)
            {
                case 1:
                    newShift = settings.Morning;
                    shiftStr = "Morning";
                    break;
                case 2:
                    newShift = settings.WholeDay;
                    shiftStr = "WholeDay";
                    break;
            }

            existingDay.EditDay(newShift,shiftStr);
            await _uow.workingDayRepository.UpdateAsync(existingDay);
            await _uow.SaveChangesAsync();

        }
    }
}

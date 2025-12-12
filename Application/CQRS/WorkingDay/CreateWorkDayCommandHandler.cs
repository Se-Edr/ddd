using Domain.Models.ServiceSetting;
using Domain.Models.Termin;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.WorkingDay
{
    public record CreateWorkingDayCommand(DateTime day,int shiftId=2) :ICommand;
    public class CreateWorkDayCommandHandler(IUnitOfWork _uow) : IRequestHandler<CreateWorkingDayCommand>
    {
        public async Task Handle(CreateWorkingDayCommand request)
        {
            ServiceSettings settings = await _uow.serviceSettingsRepository.GetSettings();

            DateOnly existingDayDate= DateOnly.FromDateTime(request.day);
            //Domain.Models.Termin.WorkingDay existingDay =

            Domain.Models.Termin.WorkingDay? existingDay = await _uow.workingDayRepository.GetDayByDate(existingDayDate);
               

            if (existingDay!=null)
            {
                throw new Exception($"day already exists {existingDay.Date}");
            }

            Shift desiredShift=settings.WholeDay;
            string shiftStr = null;

            switch (request.shiftId)
            {
                case 1:
                    desiredShift = settings.Morning;
                    shiftStr = "Morning";
                    break;
                case 2:
                    desiredShift = settings.WholeDay;
                    shiftStr = "WholeDay";
                    break;
            }

            Domain.Models.Termin.WorkingDay day = 
                Domain.Models.Termin.WorkingDay.CreateDay(request.day,desiredShift,shiftStr);

            await _uow.workingDayRepository.AddAsync(day);
            await _uow.SaveChangesAsync();

        }
    }
}

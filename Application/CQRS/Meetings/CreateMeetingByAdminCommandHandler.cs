using Domain.Models.Appointment;
using Domain.Models.Operation;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Meetings
{

    public record CreateMeetingByAdminCommand(Guid dayId,
        List<Guid> procedures,
        string CarSpz,
        Guid? CarId,
        string description) :ICommand;


    public class CreateMeetingByAdminCommandHandler(IUnitOfWork _uow) :
        IRequestHandler<CreateMeetingByAdminCommand>
    {
        public async Task Handle(CreateMeetingByAdminCommand request)
        {
            

            Domain.Models.Termin.WorkingDay? assumedDay = await
                _uow.workingDayRepository.GetByIdAsync(request.dayId);

            if (assumedDay == null)
            {
                assumedDay = null;
                //throw new Exception("No termins on this day, please register termin first");
            }

            List<Procedure> procedures = [];
            int assumedPrice = 0;

            if (request.procedures.Count > 0)
            {
                foreach (Guid id in request.procedures)
                {
                    Procedure concrete = await _uow.procedureRepository.GetByIdAsync(id);
                    if (concrete != null)
                    {
                        procedures.Add(concrete);
                    }
                }
                 assumedPrice= procedures.Select(x => x.Price).Sum();
            }
           

            Meeting newMeet= Meeting.CreateMeet(assumedDay,procedures,
                request.description.ToLower(),assumedPrice,
                request.CarSpz,request.CarId);

            await _uow.meetingRepository.AddAsync(newMeet);


            throw new NotImplementedException();
        }
    }
}

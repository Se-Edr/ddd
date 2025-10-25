using Domain.Models.Operation;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Meetings
{
    public record CreateMeetingCommand(DateTime dateTime, List<Guid> procedures):
        ICommand<CreateMeetingResponse>;
    public record CreateMeetingResponse(DateTime dateTime,int price, Guid meetId);
    public class CreateMeetingCommandHandler(IUnitOfWork _uow) : 
        IRequestHandler<CreateMeetingCommand,CreateMeetingResponse>
    {
        public async Task<CreateMeetingResponse> Handle(CreateMeetingCommand request)
        {
            DateOnly date = DateOnly.FromDateTime(request.dateTime);

            Domain.Models.Termin.WorkingDay? assumedDay = await
                _uow.workingDayRepository.GetDayByDate(date);

            List<Procedure> procedures = [];

            foreach(Guid id in request.procedures)
            {
                Procedure concrete = await _uow.procedureRepository.GetByIdAsync(id);
                if (concrete != null)
                {
                    procedures.Add(concrete);
                }
            }

            int assumedPrice = procedures.Select(x => x.Price).Sum();



            throw new NotImplementedException();
        }
    }
}

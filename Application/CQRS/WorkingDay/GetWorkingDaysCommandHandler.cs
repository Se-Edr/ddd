using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.WorkingDay
{
    public record GetWorkingDaysCommand(DateTime start, DateTime finish):ICommand<List<Domain.Models.Termin.WorkingDay>>;
    public class GetWorkingDaysCommandHandler(IUnitOfWork _uow) : IRequestHandler<GetWorkingDaysCommand, List<Domain.Models.Termin.WorkingDay>>
    {
        public async Task<List<Domain.Models.Termin.WorkingDay>> Handle(GetWorkingDaysCommand request)
        {
            var d = await _uow.workingDayRepository.GetDays(request.start,request.finish);

            return d;

        }
    }
}

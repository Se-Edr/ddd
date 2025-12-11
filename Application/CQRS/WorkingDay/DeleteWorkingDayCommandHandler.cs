using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.WorkingDay
{

    public record DeleteWorkingDayCommand(DateTime day) : ICommand;
    public class DeleteWorkingDayCommandHandler(IUnitOfWork _uow) : IRequestHandler<CreateWorkingDayCommand>
    {
        public Task Handle(CreateWorkingDayCommand request)
        {
            throw new NotImplementedException();
        }
    }
}

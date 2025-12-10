using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Employee
{
    public record EmployeeResponse(Guid id,double HourlyRate,string name);
    public record GetEmployeesCommand() : ICommand<List<EmployeeResponse>>;
    public class GetEmployeesCommandHandler(IUnitOfWork _uow) : IRequestHandler<GetEmployeesCommand, List<EmployeeResponse>>
    {
        public async Task<List<EmployeeResponse>> Handle(GetEmployeesCommand request)
        {
            var r=await _uow.employeeRepository.GetAllEmployees();

            var resp=r.Select(x=>new EmployeeResponse(x.Id, x.HourlyBase, x.EmployeeName)).ToList();
            return resp;
        }
    }
}

using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Employee
{
    public record CreateEmployeeCommand(double hourlyBase,string name) : ICommand<Domain.Models.Employees.Employee>;
    public class CreateEmployeeCommandHandler(IUnitOfWork _uow) : IRequestHandler<CreateEmployeeCommand, Domain.Models.Employees.Employee>
    {
        public async Task<Domain.Models.Employees.Employee> Handle(CreateEmployeeCommand request)
        {
            Domain.Models.Employees.Employee newEmployee = Domain.Models.Employees.Employee.CreateEmployee(request.hourlyBase,request.name);
            await _uow.employeeRepository.AddAsync(newEmployee);
            await _uow.SaveChangesAsync();
            return newEmployee; 
        }
    }
}

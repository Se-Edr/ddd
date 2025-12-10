using Domain.Models.Employees;


namespace Domain.Repositories
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
        Task<Employee> GetEmployeeByName(string name);
        Task<List<Employee>> GetAllEmployees();
    }
}


using Domain.Models.Employees;
using Domain.Repositories;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository(DatabaseContext _context) : IEmployeeRepository
    {
        public async Task AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public Task<Employee?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}

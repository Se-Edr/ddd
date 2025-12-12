using Domain.Models.Operation;
using Domain.Repositories;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class ProcedureRepository(DatabaseContext _context) : IProcedureRepository
    {
        public async Task AddAsync(Procedure entity)
        {
            await _context.ProceduresTable.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Procedure?> GetByIdAsync(Guid id)
        {
            Procedure? foundedProcedure = await _context.ProceduresTable
                .FirstOrDefaultAsync(x => x.ProcedureId.Equals(id));


            if (foundedProcedure==null)
            {
                throw new Exception("Procedure doesnt exist");
            }
            return foundedProcedure;
        }


        public async Task<List<Procedure>> GetProcedures(int page, int count = 20)
        {
            IQueryable<Procedure> proceds = _context.ProceduresTable.Skip(page * count).Take(count);

            return await proceds.ToListAsync();
        }

        public Task UpdateAsync(Procedure entity)
        {
            throw new NotImplementedException();
        }

        public async  Task<Procedure?> GetProcedureByName(string name)
        {
            Procedure? existingProcedure = await _context.ProceduresTable
                .FirstOrDefaultAsync(p => p.ProcedureName.Equals(name));

            if (existingProcedure ==null)
            {
                return null;
            }
            return existingProcedure;
        }

        public async Task<List<Procedure>> GetNonFixedPrice()
        {
            return await _context.ProceduresTable.Where(x => x.fixedPrice == false).ToListAsync();
        }
    }
}

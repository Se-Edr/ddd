using Domain.Models.Operation;

namespace Domain.Repositories
{
    public interface IProcedureRepository:IRepository<Procedure>
    {
        Task<List<Procedure>> GetProcedures(int page,int count=30);
        Task<Procedure?> GetProcedureByName(string name);
        Task<List<Procedure>> GetNonFixedPrice();
    }
}

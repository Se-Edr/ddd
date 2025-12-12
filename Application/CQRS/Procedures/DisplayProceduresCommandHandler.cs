using Domain.Models.Operation;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;


namespace Application.CQRS.Procedures
{
    public record DisplayProceduresQuery(int page=0):IQuery<List<Procedure>>;
    public class DisplayProceduresCommandHandler(IUnitOfWork _uow) : IRequestHandler<DisplayProceduresQuery, List<Procedure>>
    {
        public async Task<List<Procedure>> Handle(DisplayProceduresQuery request)
        {
            var procedures = await _uow.procedureRepository.GetProcedures(request.page) ;

            return procedures;
        }
    }
}

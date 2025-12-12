using Domain.Models.Operation;
using Domain.Models.ServiceSetting;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Procedures
{

    public record CreateProcedureCommand(string name,int windows,int? price):ICommand;
    public class CreateProcedureCommandHandler(
        IUnitOfWork _uow,
        IProcedureFactory _procedureFactory
        ) : IRequestHandler<CreateProcedureCommand>
    {
        public async Task Handle(CreateProcedureCommand request)
        {
            ServiceSettings settings = await _uow.serviceSettingsRepository.GetSettings();

            Procedure? existingProcedure = await _uow.procedureRepository.GetProcedureByName(request.name);
                
              

            if(existingProcedure != null)
            {
                throw new Exception("Procedure already exists");
            }

            Procedure newProcedure =
                await _procedureFactory.CreateProcedure(request.name.ToLower(),request.windows);

            await _uow.procedureRepository.AddAsync(newProcedure);
            await _uow.SaveChangesAsync();

        }
    }
}

using Domain.Models.Operation;
using Domain.Models.ServiceSetting;
using Domain.Repositories;
using Mediatator.Core.ComsQueries;

namespace Application.CQRS.Procedures
{

    public record EditProcedureCommand(Guid id,string name,int windows,int? price):ICommand;
    public class EditProcedureCommandHandler
        (
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<EditProcedureCommand>
    {
        public async Task Handle(EditProcedureCommand request)
        {
            ServiceSettings settings = await _unitOfWork.serviceSettingsRepository.GetSettings();

            Procedure? existingProcedure = await _unitOfWork
                .procedureRepository.GetByIdAsync(request.id);

            if (existingProcedure == null)
            {
                throw new Exception("Procedure doesnt exists");
            }

            existingProcedure.UpdateProcedure(request.name,
                request.windows,
                settings.BasePricePerWindow,
                request.price
                );

            await _unitOfWork.SaveChangesAsync();

        }
    }
}

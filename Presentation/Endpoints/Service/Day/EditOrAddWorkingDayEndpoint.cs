
using Application.CQRS.WorkingDay;
using Carter;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Service.Day
{
    public class EditOrAddWorkingDayEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/days/eitOrAdd", async ([FromBody] EditOrAddRequest list, IMediator mediator) =>
            {
                foreach(var r in list.days)
                {
                    if (r.add  && !r.day.HasValue)
                    {
                        CreateWorkingDayCommand create = new CreateWorkingDayCommand(r.day.Value,r.shiftId.Value);
                      //  await mediator.Send(create);
                    }
                    if (r.edit && r.day.HasValue)
                    {
                        EditWorkingDayCommand edit = new EditWorkingDayCommand(r.DayId.Value , r.shiftId.Value);
                        await mediator.Send(edit);
                    }
                    if(r.delete)
                    {
                        //delete
                    }
                }
            });
        }
    }

    public record EditOrCreateWorkingDay(Guid? DayId, DateTime? day, int? shiftId,bool add,bool edit,bool delete);

    public record EditOrAddRequest(List<EditOrCreateWorkingDay> days);
}

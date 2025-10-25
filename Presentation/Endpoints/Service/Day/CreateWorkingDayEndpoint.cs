using Application.CQRS.WorkingDay;
using Carter;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Service.Day
{
    public class CreateWorkingDayEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/days", async ([FromBody] NewWorkingDaysList list, IMediator mediator) =>
            {
                foreach(var r in list.days)
                {
                    await mediator.Send(r);
                }
                return Results.Ok();
            });
        }
    }

    public record NewWorkingDaysList(List<CreateWorkingDayCommand> days);
}

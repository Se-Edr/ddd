using Application.CQRS.WorkingDay;
using Carter;
using Domain.Models.Termin;
using Infrastructure.DataBase;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Service.Day
{
    public class GetMyWorkingDaysEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/days", async (IMediator mediatr) => {

                var df = new GetWorkingDaysCommand(DateTime.UtcNow, DateTime.UtcNow);
                List<WorkingDay> days = await mediatr.Send(df);
                Days daysreturn = new Days(days);

                return Results.Ok(daysreturn);
            });
        }
    }

    public record Days(List<WorkingDay> workingDays);
}

using Application.CQRS.Meetings;
using Carter;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Appointments
{
    public class CreateAppointmentByAdminEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/appoinmentByAdmin", async ([FromBody] CreateMeetingByAdminCommand comm,
                IMediator mediator) =>
            {
                await mediator.Send(comm);
                return Results.Ok();
            });
        }
    }
}

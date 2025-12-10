using Application.CQRS.Employee;
using Carter;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Service.Employee
{
    public class CreateEmployeeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/employee", async ([FromBody] CreateEmployeeCommand command,IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.Ok();
            });
        }
    }
}

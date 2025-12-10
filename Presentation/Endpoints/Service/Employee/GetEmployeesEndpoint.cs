using Application.CQRS.Employee;
using Carter;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Endpoints.Service.Employee
{
    public class GetEmployeesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/employee", async (IMediator mediator) =>
            {
                List<EmployeeResponse> res=await mediator.Send(new GetEmployeesCommand());
                return Results.Ok(res);
            });
        }
    }
}

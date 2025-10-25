using Application.CQRS.Procedures;
using Carter;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Presentation.Endpoints.Service
{
    public class CreateProcedureEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/procedure", async 
                ([FromBody] CreateProcedureCommand comm,IMediator mediator) => 
            {
                await mediator.Send(comm);
                return Results.Ok();
            });
        }
    }
}

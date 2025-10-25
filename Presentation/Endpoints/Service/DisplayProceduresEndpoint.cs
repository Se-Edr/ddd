

using Application.CQRS.Procedures;
using Carter;
using Domain.Models.Operation;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Service
{
    public class DisplayProceduresEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/procedure", async (IMediator mediator) =>
            {
                var d=await mediator.Send(new DisplayProceduresQuery());
                return Results.Ok(new Procedures(d));
            });
        }
    }
    public record Procedures(List<Procedure> serviceProcedures);
}

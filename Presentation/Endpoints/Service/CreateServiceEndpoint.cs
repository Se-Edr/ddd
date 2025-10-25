using Application.CQRS.Settings;
using Application.CQRS.TestingHandl;
using Carter;
using Domain.Models.ServiceSetting;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints.Service
{
    public class CreateServiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/settings", async ([FromBody] CreateServiceCommand comm, IMediator mediatator) =>
            {
                ServiceSettings settings=await mediatator.Send(comm);
                return Results.Ok(settings);
            })
                .WithTags("SETTINGS")
                .WithName("CREATE SETTINGS");
        }
    }
}

using Application.CQRS.Settings;
using Carter;
using Domain.Models.ServiceSetting;
using Mediatator.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace Presentation.Endpoints.Service
{
    public class DisplayServiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/settings", async (IMediator mediatr) =>
            {
                ServiceSettings settings = await mediatr.Send(new DsiplayCommandSettingsCommand());
                return Results.Ok(settings);
            });
        }
    }
}

using Application.CQRS.Procedures;
using Application.Pipeline;
using Application.Validators.Procedure;
using Domain.Models.Operation;
using Domain.Models.ServiceSetting;
using Domain.Repositories;
using FluentValidation;
using Mediatator.Core;
using Mediatator.Core.Behs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions.AppExts
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.RegisterMediatator(typeof(ApplicationExtensions).Assembly);

            services.AddTransient(typeof(IPepePigBehaviour<>),typeof(ValidationBehaviour<>));
            services.AddScoped<IValidator<CreateProcedureCommand>, CreateProcedureValidator>();

            //services.AddScoped<TestCommandHandler>();
            services.AddScoped<IProcedureFactory, ProcedureFactory>();
        }
    }
}

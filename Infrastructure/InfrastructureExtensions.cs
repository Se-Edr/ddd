using Domain.Events.Service;
using Domain.Repositories;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static void AddInfastructure(this IServiceCollection services,IConfiguration config)
        {
            string connstr = config.GetConnectionString("AppointmentsConnectionString");

            services.AddDbContext<DatabaseContext>(opts=>opts.UseSqlServer(connstr));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceSettingRepository, SettingsRepository>();
            services.AddScoped<IProcedureRepository, ProcedureRepository>();
            services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IDomainDispatcher, DomainEventDispatcher>();
            services.AddScoped<IDomainEventhandler<SettingsUpdatedDomainEvent>, RecalculateProcedureHandler>();
            
        } 
    }
}

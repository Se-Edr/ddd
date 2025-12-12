using Domain.Events;
using Domain.Events.Service;
using Domain.Repositories;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static void AddInfastructure(this IServiceCollection services,IConfiguration config)
        {
            string connstr = config.GetConnectionString("AppointmentsConnectionString");

            services.AddDbContext<DatabaseContext>(opts=>opts.UseSqlServer(connstr));

            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = "192.168.1.175:1239";
                opts.InstanceName = "ServiceSettings_";
            });


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<SettingsRepository>();
            services.AddScoped<IServiceSettingRepository>(sp =>
            {
                var context= sp.GetRequiredService<DatabaseContext>();
                var tracker = sp.GetRequiredService<ITrackedEntitiesCollection>();
                var cache = sp.GetRequiredService<IDistributedCache>();

                var baseRepo= new SettingsRepository(context,tracker);
                var cachedRepo = new CachedSettingsRepository(baseRepo,cache);

                return cachedRepo;
            });


            services.AddScoped<IProcedureRepository, ProcedureRepository>();
            services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IDomainDispatcher, DomainDispatcher>();
            services.AddScoped<ITrackedEntitiesCollection, TrackedEntitesCollection>();
            services.AddScoped<IDomainEventHandler<ServiceBasePriceUpdatedEvent>, ServiceBasePriceUpdatedHandler>();
            
        } 
    }
}

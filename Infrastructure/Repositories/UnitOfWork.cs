using Domain.Events.Service;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.DataBase;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IProcedureRepository procedureRepository { get; }
        public IServiceSettingRepository serviceSettingsRepository { get; }
        public IWorkingDayRepository workingDayRepository { get; }
        public IMeetingRepository meetingRepository { get; }
        public IEmployeeRepository employeeRepository { get; }


        private readonly IDomainDispatcher _dispatcher;
        public UnitOfWork(
            DatabaseContext context,
            IProcedureRepository procedureRepository,
            IServiceSettingRepository serviceSettingsRepository,
            IWorkingDayRepository workingDayRepository,
            IMeetingRepository meetingRepository,
            IEmployeeRepository employeeRepository,
            IDomainDispatcher dispatcher
            )
        {
            _context = context;
            this.procedureRepository = procedureRepository;
            this.serviceSettingsRepository = serviceSettingsRepository;
            this.workingDayRepository = workingDayRepository;
            this.meetingRepository = meetingRepository;
            this.employeeRepository = employeeRepository;
            _dispatcher = dispatcher;
        }

        public async Task SaveChangesAsync()
        {
            var entitiesWithEvents = _context.ChangeTracker
                .Entries<MainEntity>().Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any()).ToList();

            var allEvents = entitiesWithEvents
                .SelectMany(e=>e.DomainEvents).ToList();

            await _dispatcher.DispatchAsync(allEvents);
            foreach(var entity in entitiesWithEvents)
            {
                entity.ClearDomainEvents();
            }

            await _context.SaveChangesAsync();
        }
    }
}

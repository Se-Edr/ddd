using Domain.Events;
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


        private IDomainDispatcher dispatcher { get; }
        private ITrackedEntitiesCollection trackedEntitiesCollection { get; }
        
        public UnitOfWork(
            DatabaseContext context,
            IProcedureRepository procedureRepository,
            IServiceSettingRepository serviceSettingsRepository,
            IWorkingDayRepository workingDayRepository,
            IMeetingRepository meetingRepository,
            IEmployeeRepository employeeRepository,

            ITrackedEntitiesCollection trackedEntitiesCollection,
            IDomainDispatcher dispatcher
            )
        {
            _context = context;
            this.procedureRepository = procedureRepository;
            this.serviceSettingsRepository = serviceSettingsRepository;
            this.workingDayRepository = workingDayRepository;
            this.meetingRepository = meetingRepository;
            this.employeeRepository = employeeRepository;

            this.trackedEntitiesCollection = trackedEntitiesCollection;
            this.dispatcher = dispatcher;
        }

        public async Task SaveChangesAsync()
        {
            List<IDomainEvent> allevents = new();
            var ents= trackedEntitiesCollection.GetTrackedEntities();

            foreach (var ent in ents) 
            {
                allevents.AddRange(ent.DomainEvents);
            }

            trackedEntitiesCollection.ClearAll();

            if (allevents.Any())
            {
                await dispatcher.DispatchAsync(allevents);
            }

            await _context.SaveChangesAsync();
        }
    }
}

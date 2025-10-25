using Domain.Models.ServiceSetting;
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

        public UnitOfWork(
            DatabaseContext context,
            IProcedureRepository procedureRepository,
            IServiceSettingRepository serviceSettingsRepository,
            IWorkingDayRepository workingDayRepository,
            IMeetingRepository meetingRepository)
        {
            _context = context;
            this.procedureRepository = procedureRepository;
            this.serviceSettingsRepository = serviceSettingsRepository;
            this.workingDayRepository = workingDayRepository;
            this.meetingRepository = meetingRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

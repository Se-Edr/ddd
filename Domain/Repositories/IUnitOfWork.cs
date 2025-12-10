namespace Domain.Repositories
{


    public interface IUnitOfWork
    {
        IProcedureRepository procedureRepository { get;}
        IServiceSettingRepository serviceSettingsRepository { get;}
        IWorkingDayRepository workingDayRepository { get; }
        IMeetingRepository meetingRepository { get;}
        IEmployeeRepository employeeRepository { get; }

        Task SaveChangesAsync();

    }
}

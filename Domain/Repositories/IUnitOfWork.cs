namespace Domain.Repositories
{


    public interface IUnitOfWork
    {
        IProcedureRepository procedureRepository { get;}
        IServiceSettingRepository serviceSettingsRepository { get;}
        IWorkingDayRepository workingDayRepository { get; }
        IMeetingRepository meetingRepository { get;}

        Task SaveChangesAsync();

    }
}

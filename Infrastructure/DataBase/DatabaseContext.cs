using Domain.Models.Appointment;
using Domain.Models.Employees;
using Domain.Models.Operation;
using Domain.Models.ServiceSetting;
using Domain.Models.Termin;
using Infrastructure.DataBase.Configurations;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.DataBase
{
    public class DatabaseContext:DbContext
    {
        public DbSet<ServiceSettings> SettingsTable { get; set;}
        public DbSet<WorkingDay> DaysTable { get; set;}
        public DbSet<Procedure> ProceduresTable { get; set;}
        public DbSet<Meeting> MeetingsTable { get; set; } 
        public DbSet<Employee> Employees {  get; set;}
        public DatabaseContext(DbContextOptions opts) : base(opts) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ServiceSettingsModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProcedureModelConfiguration());
            modelBuilder.ApplyConfiguration(new WorkingDayModelConfiguration());
            modelBuilder.ApplyConfiguration(new MeetingModelConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeModelConfiguration());
        }

     }
}

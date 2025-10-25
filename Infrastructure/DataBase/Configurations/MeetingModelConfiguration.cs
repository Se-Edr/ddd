using Domain.Models.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBase.Configurations
{
    public class MeetingModelConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.HasKey(m => m.MeetId);

            builder.HasMany(m => m.Procedures);

            builder.HasOne(m => m.StartingWorkingDayDay).WithMany(d => d.meetings)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

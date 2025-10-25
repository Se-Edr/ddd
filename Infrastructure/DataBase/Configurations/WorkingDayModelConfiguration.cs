using Domain.Models.Termin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.DataBase.Configurations
{
    public class WorkingDayModelConfiguration : IEntityTypeConfiguration<WorkingDay>
    {
        public void Configure(EntityTypeBuilder<WorkingDay> builder)
        {
            builder.HasKey(d => d.DayId);

            builder.Property(d => d.Date)
                .HasConversion(
                date=>date.ToDateTime(TimeOnly.MinValue),
                date=>DateOnly.FromDateTime(date))
                .HasColumnType("date");


            builder.Property(d => d.StartTime)
                .HasConversion(
                time => time.ToTimeSpan(),
                time => TimeOnly.FromTimeSpan(time))
                .HasColumnType("time(0)");

            builder.Property(d => d.EndTime)
               .HasConversion(
               time => time.ToTimeSpan(),
               time => TimeOnly.FromTimeSpan(time))
               .HasColumnType("time(0)");

            builder.Property(d => d.ShiftString).HasMaxLength(30);

        }
    }
}

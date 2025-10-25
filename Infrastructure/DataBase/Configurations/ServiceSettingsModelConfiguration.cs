using Domain.Models.ServiceSetting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.DataBase.Configurations
{
    public class ServiceSettingsModelConfiguration : IEntityTypeConfiguration<ServiceSettings>
    {
        public void Configure(EntityTypeBuilder<ServiceSettings> builder)
        {
            builder.HasKey(serset => serset.Id);
            builder.Property(serset=>serset.Id).ValueGeneratedNever();
            builder.Property(serset => serset.BasePricePerWindow)
               .IsRequired();

            builder.Property(serset => serset.BaseWindowInMinutes)
                   .IsRequired();

            builder.ComplexProperty(s => s.Morning, c =>
            {
                c.Property(s => s.startTime).HasColumnName("MorningStartTime");
                c.Property(s => s.finishTime).HasColumnName("MorningFinishTime");
            });

            builder.ComplexProperty(s => s.WholeDay, c =>
            {
                c.Property(s => s.startTime).HasColumnName("WholeDayStartTime");
                c.Property(s => s.finishTime).HasColumnName("WholeDayFinishTime");
            });

        }
    }
}

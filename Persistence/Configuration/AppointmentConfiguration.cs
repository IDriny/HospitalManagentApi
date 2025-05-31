using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagentApi.Persistence.Configration
{
    public class AppointmentConfiguration:IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(a => a.DoctorId)
                .IsRequired();
            builder.Property(a => a.PatientId)
                .IsRequired();
            builder.Property(a => a.Date)
                .IsRequired();

            builder.HasOne(c => c.Doctor)
                .WithMany(d => d.Appointment)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointment)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

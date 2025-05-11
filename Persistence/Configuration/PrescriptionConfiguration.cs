using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagentApi.Persistence.Configuration
{
    public class PrescriptionConfiguration:IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.Property(p => p.Description).IsRequired();
            builder.HasOne(p => p.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(p => p.PatientId);
        }
    }
}

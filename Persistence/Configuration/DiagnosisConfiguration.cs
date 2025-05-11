using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagentApi.Persistence.Configuration
{
    public class DiagnosisConfiguration:IEntityTypeConfiguration<Diagnosis>
    {
        public void Configure(EntityTypeBuilder<Diagnosis> builder)
        {
            builder.Property(d => d.Description).IsRequired();

            builder.HasOne(d => d.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientID);
        }
    }
}

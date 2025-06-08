using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagentApi.Persistence.Configuration
{
    public class LaboratoryConfiguration:IEntityTypeConfiguration<Laboratory>
    {
        public void Configure(EntityTypeBuilder<Laboratory> builder)
        {
            builder.HasOne(l => l.Patient)
                .WithMany(p => p.Laboratories)
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(l => l.PatientId);
        }
    }
}

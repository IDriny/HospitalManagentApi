using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagentApi.Persistence.Configuration
{
    public class ClinicDoctorsConfiguration:IEntityTypeConfiguration<ClinicDoctor>
    {
        public void Configure(EntityTypeBuilder<ClinicDoctor> builder)
        {
            builder.HasOne(c => c.Doctor)
                .WithMany(d => d.Clinic)
                .HasForeignKey(c => c.DoctorId);

            builder.HasOne(c => c.Clinic)
                .WithMany(c => c.Doctors)
                .HasForeignKey(c => c.DoctorId);
        }
    }
}

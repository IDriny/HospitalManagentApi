using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagentApi.Persistence.Configuration
{
    public class ClinicDoctorsConfiguration:IEntityTypeConfiguration<ClinicDoctor>
    {
        public void Configure(EntityTypeBuilder<ClinicDoctor> builder)
        {
            // Define the many-to-many relationship between Doctor and Clinic via ClinicDoctor
            builder.HasOne(cd => cd.Doctor)
                .WithMany(d => d.Clinic) // Doctor has many ClinicDoctors
                .HasForeignKey(cd => cd.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Avoid cascading deletes if needed

            builder.HasOne(cd => cd.Clinic)
                .WithMany(c => c.Doctors) // Clinic has many ClinicDoctors
                .HasForeignKey(cd => cd.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Avoid cascading deletes if needed

            // Ensure the join table has proper indexes
            builder.HasIndex(cd => cd.DoctorId);
            builder.HasIndex(cd => cd.ClinicId);
        }
    }
}

using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagentApi.Persistence.Configration
{
    public class ClinicConfiguration:IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.Property(c => c.Name).IsRequired();
        }
    }
}

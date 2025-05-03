using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence
{
    public class HospitalDbContext : DbContext
    {


        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {

        }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }




        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().Property(a => a.DoctorId)
                .IsRequired();
            modelBuilder.Entity<Appointment>().Property(a => a.PatientId)
                .IsRequired();
            modelBuilder.Entity<Appointment>().Property(a => a.Date)
                .IsRequired();

            modelBuilder.Entity<Appointment>().HasOne(c => c.Doctor)
                .WithMany(d => d.Appointment)
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<Appointment>().HasOne(a => a.Patient)
                .WithMany(p => p.Appointment)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Clinic>().Property(c => c.Id).IsRequired();

            modelBuilder.Entity<Clinic>().HasMany(c => c.Doctors).WithMany(d => d.Clinic);

        }
    }
}

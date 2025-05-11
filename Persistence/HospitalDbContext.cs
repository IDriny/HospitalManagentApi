using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Persistence.Configration;
using HospitalManagentApi.Persistence.Configuration;
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
        public  DbSet<ClinicDoctor> ClinicDoctors { get; set; }
        public DbSet<Diagnosis> Diagnoses{ get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }




        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicDoctorsConfiguration());
            modelBuilder.ApplyConfiguration(new DiagnosisConfiguration());
            modelBuilder.ApplyConfiguration(new PrescriptionConfiguration());

            //Appointment Configration
            //modelBuilder.Entity<Appointment>().Property(a => a.DoctorId)
            //    .IsRequired();
            //modelBuilder.Entity<Appointment>().Property(a => a.PatientId)
            //    .IsRequired();
            //modelBuilder.Entity<Appointment>().Property(a => a.Date)
            //    .IsRequired();

            //modelBuilder.Entity<Appointment>().HasOne(c => c.Doctor)
            //    .WithMany(d => d.Appointment)
            //    .HasForeignKey(a => a.DoctorId);

            //modelBuilder.Entity<Appointment>().HasOne(a => a.Patient)
            //    .WithMany(p => p.Appointment)
            //    .HasForeignKey(a => a.PatientId);

            //clinic config
            //modelBuilder.Entity<Clinic>().Property(c => c.Name).IsRequired();

            //ClinicDoctor config

            //modelBuilder.Entity<ClinicDoctor>()
            //    .HasOne(c => c.Doctor)
            //    .WithMany(d => d.Clinic)
            //     .HasForeignKey(c => c.DoctorId);


            //modelBuilder.Entity<ClinicDoctor>()
            //    .HasOne(c => c.Clinic)
            //    .WithMany(c => c.Doctors)
            //    .HasForeignKey(c => c.DoctorId);


            //Diagnosis Configration
            //modelBuilder.Entity<Diagnosis>().Property(d => d.Description).IsRequired();

            //modelBuilder.Entity<Diagnosis>().HasOne(d => d.Patient)
            //    .WithMany(p => p.Diagnoses)
            //    .HasForeignKey(d=>d.PatientID);

            //prescription config
            //modelBuilder.Entity<Prescription>().Property(p => p.Description).IsRequired();

            //modelBuilder.Entity<Prescription>().HasOne(p => p.Patient)
            //   .WithMany(p => p.Prescriptions)
            //   .HasForeignKey(p => p.PatientId);


        }
    }
}

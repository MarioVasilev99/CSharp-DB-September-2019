namespace P01_HospitalDatabase.Data
{
    using P01_HospitalDatabase.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataSettings.DefaultConnection);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Prescriptions)
                .WithOne(pr => pr.Patient)
                .HasForeignKey(p => p.PatientId);

            modelBuilder
                .Entity<Medicament>()
                .HasMany(m => m.Prescriptions)
                .WithOne(pr => pr.Medicament)
                .HasForeignKey(m => m.MedicamentId);

            modelBuilder
                .Entity<PatientMedicament>()
                .HasKey(pk => new { pk.PatientId, pk.MedicamentId });

            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(v => v.Patient)
                .HasForeignKey(p => p.VisitationId);

            modelBuilder
                .Entity<Visitation>()
                .HasOne(v => v.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(p => p.VisitationId);

            modelBuilder
                .Entity<Diagnose>()
                .HasOne(d => d.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(p => p.DiagnoseId);

            modelBuilder
                .Entity<Doctor>()
                .HasMany(d => d.Visitations)
                .WithOne(v => v.Doctor)
                .HasForeignKey(v => v.DoctorId);
        }
    }
}

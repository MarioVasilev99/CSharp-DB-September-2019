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
        }
    }
}

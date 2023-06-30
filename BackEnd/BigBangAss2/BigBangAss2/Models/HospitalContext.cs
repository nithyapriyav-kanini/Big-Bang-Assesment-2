using Microsoft.EntityFrameworkCore;

namespace BigBangAss2.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions opts) : base(opts)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Email})
                .IsUnique(true);
        }
    }
}

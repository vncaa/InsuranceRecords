using Insurance.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<InsuranceRecord> InsuranceRecords { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserInsurance>().HasKey(userIns => new
            {
                userIns.UserId,
                userIns.InsuranceId
            });

            modelBuilder.Entity<UserInsurance>()
                    .HasOne(u => u.User)
                    .WithMany(u => u.UserInsuranceRecords)
                    .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserInsurance>()
                   .HasOne(u => u.InsuranceRecord)
                   .WithMany(u => u.UserInsuranceRecords)
                   .HasForeignKey(u => u.InsuranceId);
        }
    }
}

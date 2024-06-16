using EmployeeWorkingHoursTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWorkingHoursTracker.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 

        }
            
        public DbSet<Employee> employees { get; set; }

        public DbSet<TimeTracking> timeTrackings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeTracking>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.TimeTrackings)
                .HasForeignKey(t => t.EmployeeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

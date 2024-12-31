using CarManagement.Data.Entities;
using CarManagement.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CarManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarGarage> CarsGarages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarGarage>()
                .HasKey(cg => new { cg.CarId, cg.GarageId });

            modelBuilder.Entity<CarGarage>()
                .HasOne(cg => cg.Car)
                .WithMany(c => c.CarGarages)
                .HasForeignKey(cg => cg.CarId);

            modelBuilder.Entity<CarGarage>()
                .HasOne(cg => cg.Garage)
                .WithMany()
                .HasForeignKey(cg => cg.GarageId);
        }
    }
}

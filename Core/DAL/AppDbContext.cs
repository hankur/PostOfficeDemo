using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Bag> Bags { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            const int precision = 18;
            const int weightScale = 3;
            const int priceScale = 2;

            builder.Entity<Parcel>()
                .Property(p => p.Weight)
                .HasPrecision(precision, weightScale);
            builder.Entity<Parcel>()
                .Property(p => p.Price)
                .HasPrecision(precision, priceScale);

            builder.Entity<Bag>()
                .Property(b => b.Weight)
                .HasPrecision(precision, weightScale);
            builder.Entity<Bag>()
                .Property(b => b.Price)
                .HasPrecision(precision, priceScale);
        }
    }
}
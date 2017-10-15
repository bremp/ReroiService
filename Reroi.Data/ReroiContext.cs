using Microsoft.EntityFrameworkCore;
using Reroi.Model.Entities;
using System.Linq;

namespace Reroi.Data
{
    public class ReroiContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Property> Properties { get; set; }


        public ReroiContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            modelBuilder.Entity<Country>()
                .ToTable("Country");

            modelBuilder.Entity<Country>()
                .Property(s => s.Name)
                .IsRequired();

            modelBuilder.Entity<Country>()
                .Property(s => s.EpiIndex)
                .IsRequired();

            modelBuilder.Entity<Property>()
                .ToTable("Property");

            modelBuilder.Entity<Property>()
                .Property(s => s.Mls)
                .IsRequired();

            modelBuilder.Entity<Property>()
                .Property(s => s.PurchasePrice)
                .IsRequired();
        }
    }
}

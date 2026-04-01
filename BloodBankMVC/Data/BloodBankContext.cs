using BloodBankMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BloodBankMVC.Data
{
    public class BloodBankContext : DbContext
    {
        public BloodBankContext(DbContextOptions<BloodBankContext> options) : base(options)
        {
        }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Requestor> Requestors { get; set; }
        public DbSet<BloodInventory> BloodInventories { get; set; }
        public DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Blood Groups
            modelBuilder.Entity<BloodGroup>().HasData(
                new BloodGroup { Id = 1, BloodGroupName = "A+" },
                new BloodGroup { Id = 2, BloodGroupName = "A-" },
                new BloodGroup { Id = 3, BloodGroupName = "B+" },
                new BloodGroup { Id = 4, BloodGroupName = "B-" },
                new BloodGroup { Id = 5, BloodGroupName = "AB+" },
                new BloodGroup { Id = 6, BloodGroupName = "AB-" },
                new BloodGroup { Id = 7, BloodGroupName = "O+" },
                new BloodGroup { Id = 8, BloodGroupName = "O-" }
            );

            // Seed initial Collection data
            modelBuilder.Entity<BloodInventory>().HasData(
                new  { Id = 1, BloodGroupId = 1, Quantity = 0 },
                new BloodInventory { Id = 2, BloodGroupId = 2, Quantity = 0 },
                new BloodInventory { Id = 3, BloodGroupId = 3, Quantity = 0 },
                new BloodInventory { Id = 4, BloodGroupId = 4, Quantity = 0 },
                new BloodInventory { Id = 5, BloodGroupId = 5, Quantity = 0 },
                new BloodInventory { Id = 6, BloodGroupId = 6, Quantity = 0 },
                new BloodInventory { Id = 7, BloodGroupId = 7, Quantity = 0 },
                new BloodInventory { Id = 8, BloodGroupId = 8, Quantity = 0 }
            );
        }
    }
}

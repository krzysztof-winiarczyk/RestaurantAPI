using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString =
            "Server=DESKTOP-IO0A0RD\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;";

        //private string _connectionString =
        //    "Server=DESKTOP-P7U53VP;Database=RestaurantDb;Trusted_Connection=True;";

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        //to configure which entity has which property
        //we must override OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();


            modelBuilder.Entity<Restaurant>()       //determine entity
                .Property(r => r.Name)              //determine property
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .HasMaxLength(50);

            //here we can seed the db with data
            //modelBuilder.Entity<Restaurant>()
            //    .HasData();
        }

        //to configure connetction to DB
        //and which DB we want to use
        //we must override OnConfiguring method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

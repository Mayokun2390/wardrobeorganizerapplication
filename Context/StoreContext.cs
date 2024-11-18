using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WardrobeOrganizerApp.Constant;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Cart> Carts {get;set;}
        public DbSet<ChartBot> ChartBots{get;set;}
        public DbSet<Customer> Customers {get;set;}
        public DbSet<CartItem> CartItems {get;set;}
        public DbSet<Order> Orders {get;set;}
        public DbSet<OrderItem> OrderItems {get;set;}
        public DbSet<OrderProduct> OrderProducts {get;set;}
        public DbSet<Payment> Payments {get;set;}
        public DbSet<Product> Products {get;set;}
        public DbSet<Role> Roles {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<UserRole> UserRoles {get;set;}
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var UserId = Guid.NewGuid();
            var CustomerId = Guid.NewGuid();
            var AdminId = Guid.NewGuid();


            var customer = RoleConstant.Customer;
            var admin = RoleConstant.Admin;


            var username = "Mayokun";
            var Email = "admin@gmail.com";
            // var Password = "admim";
            var PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin", PasswordSalt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = UserId,
                    UserName = username,
                    Email = Email,
                    PasswordSalt = PasswordSalt,
                    PasswordHash = PasswordHash,
                }
            );
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = AdminId,
                    Name = admin,
                },
                new Role
                {
                    Id = CustomerId,
                    Name = customer,
                }
            );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    RoleId = AdminId,
                    UserId = UserId,
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
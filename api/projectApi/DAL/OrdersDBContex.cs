using Microsoft.EntityFrameworkCore;
using projectApi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Drawing;


namespace projectApi.DAL
{
    public class OrdersDBContex(DbContextOptions<OrdersDBContex> options) : DbContext(options)
    {
        public DbSet<Donor> Donor { get; set; }
        public DbSet<Present> Present { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<LoginRequest> LoginRequest { get; set; }
        public DbSet<Winners> Winners { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
        

using System;
using FCANoti.Web.Entity;
using Microsoft.EntityFrameworkCore;

namespace FCANoti.Web.Repository
{
    public class FCANotiDbContext : DbContext
    {
        public FCANotiDbContext(DbContextOptions<FCANotiDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Orders -< OrderItems - configures one-to-many relationship
            // modelBuilder.Entity<Orders>()
            //    .HasMany(o => o.OrderItems)
            //    .WithOne()
            //    .HasForeignKey(con => con.OrderID);

        }
    }
}

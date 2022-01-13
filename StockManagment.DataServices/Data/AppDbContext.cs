using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagment.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<UserWarehouses> UserWarehouses { get; set; }

        public DbSet<Contract_in> Contract_In { get; set; }

        public DbSet<Factura_In> Factura_In { get; set; }

        public DbSet<MockProduct> MockProducts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserWarehouses>()
                    .HasOne(u => u.User)
                    .WithMany(uw => uw.UserWarehouses)
                    .HasForeignKey(ui => ui.UserId);

            builder.Entity<UserWarehouses>()
                   .HasOne(u => u.Warehouse)
                   .WithMany(uw => uw.UserWarehouses)
                   .HasForeignKey(ui => ui.WarehouseId);
        }
    }
}

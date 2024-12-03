using WebClothes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebClothes.Data
{
    public class ShopContext : IdentityDbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attribute_Value> Attribute_Values { get; set; }
        public DbSet<Attribute_Pro> Attributes { get; set; }
        public DbSet<ProductUom> ProductUom { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<Img> imgs { get; set; }
        public DbSet<AttrAndPro> AttrAndPro { get; set; }   
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<SaleOrderLine> SaleOrderLines { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Attribute_Pro>();
            modelBuilder.Entity<Attribute_Value>();
            modelBuilder.Entity<ProductUom>();
            modelBuilder.Entity<CustomerProfile>();
            modelBuilder.Entity<SaleOrder>();
            modelBuilder.Entity<Province>();
            modelBuilder.Entity<District>();
            modelBuilder.Entity<Ward>();
            modelBuilder.Entity<Img>(); 
            modelBuilder.Entity<AttrAndPro>();
            modelBuilder.Entity<SaleOrderLine>();
            base.OnModelCreating(modelBuilder);

        }
    }
}

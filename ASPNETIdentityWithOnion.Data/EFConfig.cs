using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Data.Identity.Models;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.DomainModels.Identity;

namespace ASPNETIdentityWithOnion.Data
{
    public class EfConfig
    {
        public static void ConfigureEf(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationIdentityUser>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ApplicationIdentityRole>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ApplicationIdentityUserClaim>()
                 .Property(e => e.Id)
                 .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Category>()
               .Property(e => e.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.SubCategories)
                .WithRequired(f => f.Category)
                .HasForeignKey(s => s.CategoryId);
                

            modelBuilder.Entity<SubCategory>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<SubCategory>()
                .HasMany(e => e.Products)
                .WithRequired(a => a.SubCategory)
                .HasForeignKey(d => d.SubCategoryId);
                

           
            modelBuilder.Entity<Product>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Order>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<OrderDetail>()
               .Property(e => e.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithRequired(p => p.Order)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>()
                .Property(c => c.CustomerId).IsOptional();

            modelBuilder.Entity<OrderDetail>().HasRequired(p => p.Product);
            modelBuilder.Entity<OrderDetail>().Property(e=>e.OrderId).HasColumnOrder(0);
            modelBuilder.Entity<OrderDetail>().Property(e => e.ProductId).HasColumnOrder(1);


            modelBuilder.Entity<Customer>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOptional(c => c.Customer)
            .HasForeignKey(o => o.CustomerId)
            .WillCascadeOnDelete(false);

            
            modelBuilder.Entity<CartLine>()
                .Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Cart>()
                .Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Lines)
                .WithRequired(x=>x.Cart)
                .HasForeignKey(p=>p.CartId)
                .WillCascadeOnDelete(true);


            //modelBuilder.Entity<Customer>().Map(n => { n.ToTable("CustomerOder");
            //n.Properties(c=>c.Id)})
            //     .Property(e => e.Id)
            //     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
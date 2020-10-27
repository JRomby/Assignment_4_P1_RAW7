using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment4_P1New
{
    public partial class northwindContext : DbContext
    {
        public northwindContext()
        {
        }

        public northwindContext(DbContextOptions<northwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<OrderDetails> Orderdetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("host=localhost;database=northwind;user id=postgres;password = fnb78n87j;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_category");

                entity.ToTable("categories");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_categories_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("categoryid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("categoryname")
                    .HasMaxLength(15);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { Orderid = e.OrderId, Productid = e.ProductId })
                    .HasName("pk_order_detail");

                entity.ToTable("orderdetails");

                entity.HasIndex(e => new { Orderid = e.OrderId, Productid = e.ProductId })
                    .HasName("pk_order_details_idx")
                    .IsUnique();

                entity.Property(e => e.OrderId).HasColumnName("orderid");

                entity.Property(e => e.ProductId).HasColumnName("productid");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitPrice).HasColumnName("unitprice");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderdetail_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderdetail_product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_order");

                entity.ToTable("orders");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_orders_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("orderid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Freight).HasColumnName("freight");

                entity.Property(e => e.Date)
                    .HasColumnName("orderdate")
                    .HasColumnType("date");

                entity.Property(e => e.Required)
                    .HasColumnName("requireddate")
                    .HasColumnType("date");

                entity.Property(e => e.Shipaddress)
                    .HasColumnName("shipaddress")
                    .HasMaxLength(60);

                entity.Property(e => e.ShipCity)
                    .HasColumnName("shipcity")
                    .HasMaxLength(15);

                entity.Property(e => e.Shipcountry)
                    .HasColumnName("shipcountry")
                    .HasMaxLength(15);

                entity.Property(e => e.ShipName)
                    .HasColumnName("shipname")
                    .HasMaxLength(40);

                entity.Property(e => e.Shippeddate)
                    .HasColumnName("shippeddate")
                    .HasColumnType("date");

                entity.Property(e => e.Shippostalcode)
                    .HasColumnName("shippostalcode")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_product");

                entity.ToTable("products");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_products_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("productid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("productname")
                    .HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit)
                    .HasColumnName("quantityperunit")
                    .HasMaxLength(20);

                entity.Property(e => e.Supplierid).HasColumnName("supplierid");

                entity.Property(e => e.UnitPrice).HasColumnName("unitprice");

                entity.Property(e => e.UnitsInStock).HasColumnName("unitsinstock");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("fk_product_category");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

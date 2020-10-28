using Microsoft.EntityFrameworkCore;

namespace DataService
{
    public partial class northwind2Context : DbContext
    {
        public northwind2Context()
        {
        }

        public northwind2Context(DbContextOptions<northwind2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Orderdetails> Orderdetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("host=localhost;database=northwind2;user id=postgres;password = fnb78n87j;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("pk_category");

                entity.ToTable("categories");

                entity.HasIndex(e => e.Categoryid)
                    .HasName("pk_categories_idx")
                    .IsUnique();

                entity.Property(e => e.Categoryid)
                    .HasColumnName("categoryid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasColumnName("categoryname")
                    .HasMaxLength(15);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Customerid)
                    .HasName("pk_customer");

                entity.ToTable("customers");

                entity.HasIndex(e => e.Customerid)
                    .HasName("pk_customers_idx")
                    .IsUnique();

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(60);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasColumnName("companyname")
                    .HasMaxLength(40);

                entity.Property(e => e.Contactname)
                    .HasColumnName("contactname")
                    .HasMaxLength(30);

                entity.Property(e => e.Contacttitle)
                    .HasColumnName("contacttitle")
                    .HasMaxLength(30);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(15);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(24);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(24);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.Employeeid)
                    .HasName("pk_employees");

                entity.ToTable("employees");

                entity.HasIndex(e => e.Employeeid)
                    .HasName("pk_employees_idx")
                    .IsUnique();

                entity.Property(e => e.Employeeid)
                    .HasColumnName("employeeid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(60);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(15);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(10);

                entity.Property(e => e.Hiredate)
                    .HasColumnName("hiredate")
                    .HasColumnType("date");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(20);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(10);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Orderdetails>(entity =>
            {
                entity.HasKey(e => new { e.Orderid, e.Productid })
                    .HasName("pk_order_detail");

                entity.ToTable("orderdetails");

                entity.HasIndex(e => new { e.Orderid, e.Productid })
                    .HasName("pk_order_details_idx")
                    .IsUnique();

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Unitprice).HasColumnName("unitprice");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderdetail_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderdetail_product");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("pk_order");

                entity.ToTable("orders");

                entity.HasIndex(e => e.Orderid)
                    .HasName("pk_orders_idx")
                    .IsUnique();

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Freight).HasColumnName("freight");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("date");

                entity.Property(e => e.Requireddate)
                    .HasColumnName("requireddate")
                    .HasColumnType("date");

                entity.Property(e => e.Shipaddress)
                    .HasColumnName("shipaddress")
                    .HasMaxLength(60);

                entity.Property(e => e.Shipcity)
                    .HasColumnName("shipcity")
                    .HasMaxLength(15);

                entity.Property(e => e.Shipcountry)
                    .HasColumnName("shipcountry")
                    .HasMaxLength(15);

                entity.Property(e => e.Shipname)
                    .HasColumnName("shipname")
                    .HasMaxLength(40);

                entity.Property(e => e.Shippeddate)
                    .HasColumnName("shippeddate")
                    .HasColumnType("date");

                entity.Property(e => e.Shippostalcode)
                    .HasColumnName("shippostalcode")
                    .HasMaxLength(10);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("fk_order_customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Employeeid)
                    .HasConstraintName("fk_order_employee");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("pk_product");

                entity.ToTable("products");

                entity.HasIndex(e => e.Productid)
                    .HasName("pk_products_idx")
                    .IsUnique();

                entity.Property(e => e.Productid)
                    .HasColumnName("productid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasColumnName("productname")
                    .HasMaxLength(40);

                entity.Property(e => e.Quantityperunit)
                    .HasColumnName("quantityperunit")
                    .HasMaxLength(20);

                entity.Property(e => e.Supplierid).HasColumnName("supplierid");

                entity.Property(e => e.Unitprice).HasColumnName("unitprice");

                entity.Property(e => e.Unitsinstock).HasColumnName("unitsinstock");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("fk_product_category");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Supplierid)
                    .HasConstraintName("fk_product_supplier");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.Supplierid)
                    .HasName("pk_supplier");

                entity.ToTable("suppliers");

                entity.HasIndex(e => e.Supplierid)
                    .HasName("pk_suppliers_idx")
                    .IsUnique();

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(60);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasColumnName("companyname")
                    .HasMaxLength(40);

                entity.Property(e => e.Contactname)
                    .HasColumnName("contactname")
                    .HasMaxLength(30);

                entity.Property(e => e.Contacttitle)
                    .HasColumnName("contacttitle")
                    .HasMaxLength(30);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(15);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(24);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(24);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

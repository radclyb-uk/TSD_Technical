using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.Contexts
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public string DbPath { get; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            path = Path.Join(path, "TSD_BenRadclyffe");
            if (!Path.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DbPath = Path.Join(path, "Orders.db");
        }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItems");
                entity.HasKey(oi => oi.Id);

                entity.Property(oi => oi.Id).ValueGeneratedOnAdd();
                entity.Property(oi => oi.ProductName).IsRequired().HasMaxLength(50);
                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            });

            // Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(o => o.Id);
                entity.HasMany(o => o.Items)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId);

                entity.Property(o => o.Id).ValueGeneratedOnAdd();
                entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(o => o.OrderDate).IsRequired();
                entity.Property(o => o.Status).IsRequired().HasDefaultValue(OrderStatus.Pending);
            });
        }
    }
}

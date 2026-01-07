using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<DeliveryType> DeliveryTypes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.DishId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Available).IsRequired();
            entity.Property(e => e.ImageUrl).IsRequired();
            entity.Property(e => e.CreateDate).IsRequired();
            entity.Property(e => e.UpdateDate);

            entity.HasOne(d => d.Category)
                  .WithMany(c => c.Dishes)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasMany(d => d.OrderItems)
                  .WithOne(oi => oi.Dish)
                  .HasForeignKey(oi => oi.DishId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.Notes);
            entity.Property(e => e.CreateDate).IsRequired();

            entity.HasOne(oi => oi.Dish)
                  .WithMany(d => d.OrderItems)
                  .HasForeignKey(oi => oi.DishId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(oi => oi.Order)
                  .WithMany(o => o.OrderItems)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Status)
                  .WithMany(s => s.OrderItems)
                  .HasForeignKey(oi => oi.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            entity.Property(e => e.DeliveryTo).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Notes);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.CreateDate).IsRequired();
            entity.Property(e => e.UpdateDate);

            entity.HasOne(o => o.DeliveryType)
                  .WithMany(dt => dt.Orders)
                  .HasForeignKey(o => o.DeliveryTypeId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasMany(o => o.OrderItems)
                  .WithOne(oi => oi.Order)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.Status)
                  .WithMany(s => s.Orders)
                  .HasForeignKey(o => o.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
            
            entity.HasMany(s => s.OrderItems)
                  .WithOne(oi => oi.Status)
                  .HasForeignKey(oi => oi.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasMany(s => s.Orders)
                  .WithOne(o => o.Status)
                  .HasForeignKey(o => o.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Order).IsRequired();
            
            entity.HasMany(c => c.Dishes)
                  .WithOne(d => d.Category)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
                        
            entity.HasMany(dt => dt.Orders)
                  .WithOne(o => o.DeliveryType)
                  .HasForeignKey(o => o.DeliveryTypeId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DeliveryType>().HasData(
            new DeliveryType { Id = 1, Name = "Delivery" },
            new DeliveryType { Id = 2, Name = "Take Away" },
            new DeliveryType { Id = 3, Name = "Dine In" }
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "Pending" },
            new Status { Id = 2, Name = "In progress" },
            new Status { Id = 3, Name = "Ready" },
            new Status { Id = 4, Name = "Delivery" },
            new Status { Id = 5, Name = "Closed" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Entradas", Description = "Pequeñas porciones para abrir el apetito antes del plato principal.", Order = 1 },
            new Category { Id = 2, Name = "Ensaladas", Description = "Opciones frescas y livianas, ideales como acompañamiento o plato principal.", Order = 2 },
            new Category { Id = 3, Name = "Minutas", Description = "Platos rápidos y clásicos de bodegón: milanesas, tortillas, revueltos.", Order = 3 },
            new Category { Id = 4, Name = "Pastas", Description = "Variedad de pastas caseras y salsas tradicionales.", Order = 5 },
            new Category { Id = 5, Name = "Parrilla", Description = "Cortes de carne asados a la parrilla, servidos con guarniciones.", Order = 4 },
            new Category { Id = 6, Name = "Pizzas", Description = "Pizzas artesanales con masa casera y variedad de ingredientes.", Order = 7 },
            new Category { Id = 7, Name = "Sandwiches", Description = "Sandwiches y lomitos completos preparados al momento.", Order = 6 },
            new Category { Id = 8, Name = "Bebidas", Description = "Gaseosas, jugos, aguas y opciones sin alcohol.", Order = 8 },
            new Category { Id = 9, Name = "Cerveza Artesanal", Description = "Cervezas de producción artesanal, rubias, rojas y negras.", Order = 9 },
            new Category { Id = 10, Name = "Postres", Description = "Clásicos dulces caseros para cerrar la comida.", Order = 10 }
        );
    }
}
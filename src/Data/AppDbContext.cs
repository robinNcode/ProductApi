using Microsoft.EntityFrameworkCore;
using ProductApi.src.Models;

namespace ProductApi.src.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // This property represents the collection of Product entities in the database. 
    // It allows you to query and manipulate Product records using Entity Framework Core.
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; } // Add this line to include the User entity

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products"); // Map to the "Products" table in the database
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            entity.Property(p => p.Price).HasColumnName("price").HasPrecision(10, 2).IsRequired();
            entity.Property(p => p.Description).HasColumnName("description").HasMaxLength(500);
            entity.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        });

        // Configure the User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users"); // Map to the "Users" table in the database
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("id");
            entity.Property(u => u.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(u => u.Email).HasColumnName("email").HasMaxLength(150).IsRequired();
            entity.Property(u => u.Phone).HasColumnName("phone").HasMaxLength(20);
            entity.Property(u => u.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
            entity.Property(u => u.AvatarUrl).HasColumnName("avatar_url").HasMaxLength(255);
            entity.Property(u => u.GoogleId).HasColumnName("google_id").HasMaxLength(100);
            entity.Property(u => u.EmailVerifiedAt).HasColumnName("email_verified_at");
            entity.Property(u => u.Status).HasColumnName("status").HasMaxLength(20).IsRequired().HasDefaultValue("active");
            entity.Property(u => u.RememberToken).HasColumnName("remember_token").HasMaxLength(100);
            entity.Property(u => u.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            entity.Property(u => u.UpdatedAt).HasColumnName("updated_at");
            entity.Property(u => u.DeletedAt).HasColumnName("deleted_at");
        });
    }
}

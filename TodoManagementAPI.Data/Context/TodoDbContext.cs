namespace TodoManagementAPI.Data.Context;

using Microsoft.EntityFrameworkCore;
using TodoManagementAPI.Core.Entities;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.Priority).HasDefaultValue(1);

            // Indexes for better query performance
            entity.HasIndex(e => e.CreatedBy);
            entity.HasIndex(e => e.IsCompleted);
            entity.HasIndex(e => e.CreatedAt);
        });
    }
}

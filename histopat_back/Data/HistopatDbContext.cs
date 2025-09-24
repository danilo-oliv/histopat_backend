using Microsoft.EntityFrameworkCore;
using histopat_back.Models;

namespace histopat_back.Data;

public class HistopatDbContext : DbContext
{
    public HistopatDbContext(DbContextOptions<HistopatDbContext> options)
        : base(options) { }

    // DbSets
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<Module> Modules { get; set; } = null!;
    public DbSet<ModuleHistory> ModuleHistories { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<TopicHistory> TopicHistories { get; set; } = null!;
    public DbSet<SubTopic> SubTopics { get; set; } = null!;
    public DbSet<SubTopicHistory> SubTopicHistories { get; set; } = null!;
    public DbSet<Slide> Slides { get; set; } = null!;
    public DbSet<SlideHistory> SlideHistories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- UserRole: chave composta ---
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.IdUser, ur.IdRoles });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.IdRoles)
            .OnDelete(DeleteBehavior.Restrict);

        // --- Configurações de coluna (SQL Server Types) ---
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.IdRoles).HasColumnType("TINYINT");
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.IdUser).HasColumnType("BIGINT");
            entity.Property(e => e.Name).HasMaxLength(150).IsRequired();
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.Property(e => e.IdModule).HasColumnType("BIGINT");
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnType("DATETIME");
            entity.Property(e => e.LastModified).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<ModuleHistory>(entity =>
        {
            entity.Property(e => e.IdModuleHistory).HasColumnType("BIGINT");
            entity.Property(e => e.ModificationDate).HasColumnType("DATETIME");
            entity.Property(e => e.SnapshotData).IsRequired();
            entity.Property(e => e.Operation).IsRequired();
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.Property(e => e.IdTopico).HasColumnType("BIGINT");
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnType("DATETIME");
            entity.Property(e => e.LastModified).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<TopicHistory>(entity =>
        {
            entity.Property(e => e.IdTopicHistory).HasColumnType("BIGINT");
            entity.Property(e => e.ModificationDate).HasColumnType("DATETIME");
            entity.Property(e => e.SnapshotData).IsRequired();
            entity.Property(e => e.Operation).IsRequired();
        });

        modelBuilder.Entity<SubTopic>(entity =>
        {
            entity.Property(e => e.IdSubTopico).HasColumnType("BIGINT");
            entity.Property(e => e.CreatedAt).HasColumnType("DATETIME");
            entity.Property(e => e.LastModified).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<SubTopicHistory>(entity =>
        {
            entity.Property(e => e.IdSubTopicHistory).HasColumnType("BIGINT");
            entity.Property(e => e.ModificationDate).HasColumnType("DATETIME");
            entity.Property(e => e.SnapshotData).IsRequired();
            entity.Property(e => e.Operation).IsRequired();
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.Property(e => e.IdSlide).HasColumnType("BIGINT");
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Image).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnType("DATETIME");
            entity.Property(e => e.LastModified).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<SlideHistory>(entity =>
        {
            entity.Property(e => e.IdSlideSubTopicHistory).HasColumnType("BIGINT");
            entity.Property(e => e.ModificationDate).HasColumnType("DATETIME");
            entity.Property(e => e.SnapshotData).IsRequired();
            entity.Property(e => e.Operation).IsRequired();
        });

        // --- Relacionamentos Restrict ---
        modelBuilder.Entity<ModuleHistory>()
            .HasOne(h => h.User)
            .WithMany(u => u.ModuleHistories)
            .HasForeignKey(h => h.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ModuleHistory>()
            .HasOne(h => h.Module)
            .WithMany(m => m.ModuleHistories)
            .HasForeignKey(h => h.IdModule)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TopicHistory>()
            .HasOne(h => h.User)
            .WithMany(u => u.TopicHistories)
            .HasForeignKey(h => h.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TopicHistory>()
            .HasOne(h => h.Topic)
            .WithMany(t => t.TopicHistories)
            .HasForeignKey(h => h.IdTopico)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SubTopicHistory>()
            .HasOne(h => h.User)
            .WithMany(u => u.SubTopicHistories)
            .HasForeignKey(h => h.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SubTopicHistory>()
            .HasOne(h => h.SubTopic)
            .WithMany(st => st.SubTopicHistories)
            .HasForeignKey(h => h.IdSubTopico)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SlideHistory>()
            .HasOne(h => h.User)
            .WithMany(u => u.SlideHistories)
            .HasForeignKey(h => h.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SlideHistory>()
            .HasOne(h => h.Slide)
            .WithMany(s => s.SlideHistories)
            .HasForeignKey(h => h.IdSlide)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

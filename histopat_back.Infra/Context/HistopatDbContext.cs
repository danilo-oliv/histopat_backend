

using histopat_back.Dominio.Models.Module;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Subtopic;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Dominio.Models.User;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Context;

public class HistopatDbContext : DbContext
{
    public HistopatDbContext(DbContextOptions<HistopatDbContext> options)
        : base(options) { }

    // DbSets
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<ModuleModel> Modules { get; set; } = null!;
    public DbSet<ModuleHistory> ModuleHistories { get; set; } = null!;
    public DbSet<TopicModel> Topics { get; set; } = null!;
    public DbSet<TopicHistory> TopicHistories { get; set; } = null!;
    public DbSet<SubtopicModel> SubTopics { get; set; } = null!;
    public DbSet<SubTopicHistory> SubTopicHistories { get; set; } = null!;
    public DbSet<SlideModel> Slides { get; set; } = null!;
    public DbSet<SlideHistory> SlideHistories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==============================
        // MODULE
        // ==============================
        modelBuilder.Entity<ModuleModel>(entity =>
        {
            entity.ToTable("Modules");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .HasColumnType("INT");

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Active)
                  .IsRequired();

            entity.Property(e => e.CreatedAt)
                  .HasColumnType("DATETIME")
                  .IsRequired();

            entity.Property(e => e.LastModified)
                  .HasColumnType("DATETIME")
                  .IsRequired(false);

            // Relacionamento 1:N → Module -> Topics
            entity.HasMany(e => e.Topics)
                  .WithOne(t => t.Module)
                  .HasForeignKey(t => t.IdModule)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento 1:N → Module -> ModuleHistories
            entity.HasMany(e => e.History)
                  .WithOne(h => h.Module)
                  .HasForeignKey(h => h.IdModule)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ==============================
        // MODULE HISTORY
        // ==============================
        modelBuilder.Entity<ModuleHistory>(entity =>
        {
            entity.ToTable("ModuleHistories");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .HasColumnType("INT");

            entity.Property(e => e.ChangedAt)
                  .HasColumnType("DATETIME")
                  .IsRequired();

            entity.Property(e => e.Action)
                  .IsRequired()
                  .HasMaxLength(100);

            // Relacionamento → User
            entity.HasOne(e => e.User)
                  .WithMany(u => u.ModuleHistories)
                  .HasForeignKey(e => e.IdUser)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento → Module
            entity.HasOne<ModuleModel>()
                  .WithMany(m => m.History)
                  .HasForeignKey("IdModule")
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

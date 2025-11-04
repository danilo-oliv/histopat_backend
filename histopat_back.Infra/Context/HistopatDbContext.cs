

using histopat_back.Dominio.Models.Module;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Subtopic;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Dominio.Models.User;
using Microsoft.EntityFrameworkCore;

using histopat_back.Infra.Configurations;
namespace histopat_back.Context;

public class HistopatDbContext : DbContext
{
    public HistopatDbContext(DbContextOptions<HistopatDbContext> options)
        : base(options) { }

    // DbSets
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<Module> Modules { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<Subtopic> SubTopics { get; set; } = null!;
    public DbSet<Slide> Slides { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleConfiguration());
        modelBuilder.ApplyConfiguration(new TopicConfiguration());
        modelBuilder.ApplyConfiguration(new SubtopicConfiguration());
        modelBuilder.ApplyConfiguration(new SlideConfiguration());

        base.OnModelCreating(modelBuilder);
        
    }
}

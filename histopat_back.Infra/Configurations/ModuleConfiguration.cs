using histopat_back.Dominio.Models.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace histopat_back.Infra.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable(nameof(Module));

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(m => m.ImageUrl)
                .IsRequired();

            builder.Property(m => m.Active)
                .IsRequired();

            builder.Property(m => m.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.LastModified)
                .HasColumnType("datetime2");

            builder.HasMany(m => m.Topics)
                .WithOne(t => t.Module)
                .HasForeignKey(t => t.IdModule)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

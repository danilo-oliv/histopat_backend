using histopat_back.Dominio.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace histopat_back.Infra.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));

            builder.HasKey(r => r.IdRole);

            builder.Property(r => r.IdRole)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Active)
                .IsRequired();

            builder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.IdRole)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

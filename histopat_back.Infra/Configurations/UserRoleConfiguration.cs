using histopat_back.Dominio.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace histopat_back.Infra.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(nameof(UserRole));

            builder.HasKey(ur => new { ur.IdUser, ur.IdRole });

            builder.Property(ur => ur.Active)
                .IsRequired();
        }
    }
}

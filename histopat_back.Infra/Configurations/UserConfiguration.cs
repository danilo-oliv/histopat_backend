using histopat_back.Dominio.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace histopat_back.Infra.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(u => u.IdUser);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Active)
                .HasDefaultValue(true);

            builder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.IdUser);

            builder.HasMany(u => u.ModuleHistories)
                .WithOne(mh => mh.User)
                .HasForeignKey(mh => mh.IdUser);

            builder.HasMany(u => u.TopicHistories)
                .WithOne(th => th.User)
                .HasForeignKey(th => th.IdUser);

            builder.HasMany(u => u.SubTopicHistories)
                .WithOne(sth => sth.User)
                .HasForeignKey(sth => sth.IdUser);

            builder.HasMany(u => u.SlideHistories)
                .WithOne(sh => sh.User)
                .HasForeignKey(sh => sh.IdUser);
        }
    }
}

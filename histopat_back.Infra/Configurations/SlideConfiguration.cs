using histopat_back.Dominio.Models.Slide;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace histopat_back.Infra.Configurations
{
    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable(nameof(Slide));

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(s => s.ImageUrl)
                .IsRequired();

            builder.Property(s => s.Active)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.LastModified)
                .HasColumnType("datetime2");
        }
    }
}

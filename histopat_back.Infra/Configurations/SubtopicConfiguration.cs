using histopat_back.Dominio.Models.Subtopic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace histopat_back.Infra.Configurations
{
    public class SubtopicConfiguration : IEntityTypeConfiguration<Subtopic>
    {
        public void Configure(EntityTypeBuilder<Subtopic> builder) 
        {
            builder.ToTable(nameof(Subtopic));

            builder.HasKey(st => st.Id);

            builder.Property(st => st.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(st => st.Description)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(st => st.ImageUrl)
                .IsRequired();

            builder.Property(st => st.Active)
                .IsRequired();

            builder.Property(st => st.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(st => st.LastModified)
                .HasColumnType("datetime2");

            builder.HasMany(st => st.Slides)
                .WithOne(s => s.SubTopic)
                .HasForeignKey(s => s.IdSubTopico)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

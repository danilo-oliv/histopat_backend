using histopat_back.Dominio.Models.Topic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace histopat_back.Infra.Configurations
{
    internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable(nameof(Topic));

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Active)
                .IsRequired();

            builder.Property(m => m.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.LastModified)
                .HasColumnType("datetime2");

            builder.HasMany(t => t.SubTopics)
                .WithOne(st => st.Topic)
                .HasForeignKey(st => st.IdTopic)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

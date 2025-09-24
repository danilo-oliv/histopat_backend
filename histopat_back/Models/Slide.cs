using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("Slide")]
public class Slide
{
    [Key]
    public long IdSlide { get; set; }

    public long IdSubTopico { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Image { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    [ForeignKey(nameof(IdSubTopico))]
    public SubTopic SubTopic { get; set; } = null!;

    public ICollection<SlideHistory> SlideHistories { get; set; } = new List<SlideHistory>();
}

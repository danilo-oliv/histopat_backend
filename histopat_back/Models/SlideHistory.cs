using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("SlideHistory")]
public class SlideHistory
{
    [Key]
    public long IdSlideSubTopicHistory { get; set; }

    public DateTime ModificationDate { get; set; }

    [Required]
    public string SnapshotData { get; set; } = string.Empty;

    [Required]
    public string Operation { get; set; } = string.Empty;

    public long IdSlide { get; set; }
    public long IdUser { get; set; }

    [ForeignKey(nameof(IdSlide))]
    public Slide Slide { get; set; } = null!;

    [ForeignKey(nameof(IdUser))]
    public User User { get; set; } = null!;
}

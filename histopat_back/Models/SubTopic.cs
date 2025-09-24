using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("SubTopic")]
public class SubTopic
{
    [Key]
    public long IdSubTopico { get; set; }

    public long IdTopic { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    [ForeignKey(nameof(IdTopic))]
    public Topic Topic { get; set; } = null!;

    public ICollection<SubTopicHistory> SubTopicHistories { get; set; } = new List<SubTopicHistory>();
    public ICollection<Slide> Slides { get; set; } = new List<Slide>();
}

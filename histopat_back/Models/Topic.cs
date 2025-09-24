using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("Topic")]
public class Topic
{
    [Key]
    public long IdTopico { get; set; }

    public long IdModule { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    [ForeignKey(nameof(IdModule))]
    public Module Module { get; set; } = null!;

    public ICollection<TopicHistory> TopicHistories { get; set; } = new List<TopicHistory>();
    public ICollection<SubTopic> SubTopics { get; set; } = new List<SubTopic>();
}

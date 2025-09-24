using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("TopicHistory")]
public class TopicHistory
{
    [Key]
    public long IdTopicHistory { get; set; }

    public DateTime ModificationDate { get; set; }

    [Required]
    public string SnapshotData { get; set; } = string.Empty;

    [Required]
    public string Operation { get; set; } = string.Empty;

    public long IdUser { get; set; }
    public long IdTopico { get; set; }

    [ForeignKey(nameof(IdUser))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(IdTopico))]
    public Topic Topic { get; set; } = null!;
}

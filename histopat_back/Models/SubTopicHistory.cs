using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("SubTopicHistory")]
public class SubTopicHistory
{
    [Key]
    public long IdSubTopicHistory { get; set; }

    public DateTime ModificationDate { get; set; }

    [Required]
    public string SnapshotData { get; set; } = string.Empty;

    [Required]
    public string Operation { get; set; } = string.Empty;

    public long IdSubTopico { get; set; }
    public long IdUser { get; set; }

    [ForeignKey(nameof(IdSubTopico))]
    public SubTopic SubTopic { get; set; } = null!;

    [ForeignKey(nameof(IdUser))]
    public User User { get; set; } = null!;
}

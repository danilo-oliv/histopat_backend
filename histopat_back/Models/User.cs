using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("User")]
public class User
{
    [Key]
    public long IdUser { get; set; }

    [Required, StringLength(150)]
    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; }

    // Navigation
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<ModuleHistory> ModuleHistories { get; set; } = new List<ModuleHistory>();
    public ICollection<TopicHistory> TopicHistories { get; set; } = new List<TopicHistory>();
    public ICollection<SubTopicHistory> SubTopicHistories { get; set; } = new List<SubTopicHistory>();
    public ICollection<SlideHistory> SlideHistories { get; set; } = new List<SlideHistory>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("Module")]
public class Module
{
    [Key]
    public long IdModule { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    // Navigation
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    public ICollection<ModuleHistory> ModuleHistories { get; set; } = new List<ModuleHistory>();
}

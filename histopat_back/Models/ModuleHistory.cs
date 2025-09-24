using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("ModuleHistory")]
public class ModuleHistory
{
    [Key]
    public long IdModuleHistory { get; set; }

    public DateTime ModificationDate { get; set; }

    [Required]
    public string SnapshotData { get; set; } = string.Empty;

    [Required]
    public string Operation { get; set; } = string.Empty;

    public long IdUser { get; set; }
    public long IdModule { get; set; }

    [ForeignKey(nameof(IdUser))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(IdModule))]
    public Module Module { get; set; } = null!;
}

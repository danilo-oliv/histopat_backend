using histopat_back.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Module;

[Table("ModuleHistory")]
public class ModuleHistory : BaseHistory
{
    public int IdModule { get; set; }

    [ForeignKey(nameof(IdModule))]
    public ModuleModel Module { get; set; } = null!;
}

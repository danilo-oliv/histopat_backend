using histopat_back.Dominio.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Module;

[Table("ModuleHistory")]
public class ModuleHistory : BaseHistory
{
    public int IdModule { get; set; }

    [ForeignKey(nameof(IdModule))]
    public ModuleModel Module { get; set; } = null!;
}

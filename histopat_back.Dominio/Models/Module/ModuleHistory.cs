using histopat_back.Dominio.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Module;

public class ModuleHistory : BaseHistory
{
    public int IdModule { get; set; }

    public Module Module { get; set; } = null!;
}

using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Module;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Slide;

[Table("SlideHistory")]
public class SlideHistory : BaseHistory
{
    public int IdSlide { get; set; }

    [ForeignKey(nameof(IdSlide))]
    public SlideModel Slide { get; set; } = null!;
}

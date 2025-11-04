using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Module;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Slide;

public class SlideHistory : BaseHistory
{
    public int IdSlide { get; set; }

    public Slide Slide { get; set; } = null!;
}

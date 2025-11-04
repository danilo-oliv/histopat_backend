using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Subtopic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Slide;

public class Slide : BaseEntity<SlideHistory>
{
    public int IdSubTopico { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Subtopic.Subtopic SubTopic { get; set; } = null!;

}

using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Subtopic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Slide;

[Table("Slide")]
public class SlideModel : BaseEntity<SlideHistory>
{
    public int IdSubTopico { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [ForeignKey(nameof(IdSubTopico))]
    public SubtopicModel SubTopic { get; set; } = null!;

}

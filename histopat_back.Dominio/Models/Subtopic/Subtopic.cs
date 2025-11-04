using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Topic;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Subtopic;

public class Subtopic : BaseEntity<SubTopicHistory>
{
   
    public int IdTopic { get; set; }

    public string ImageUrl { get; set; } = null!;

    public Topic.Topic Topic { get; set; } = null!;
    
    public string? Description { get; set; }

    public ICollection<Slide.Slide> Slides { get; set; } = new List<Slide.Slide>();
}

using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Topic;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Subtopic;

[Table("SubTopic")]
public class SubtopicModel : BaseEntity<SubTopicHistory>
{
   
    public int IdTopic { get; set; }

    public string ImageUrl { get; set; } = null!;

    [ForeignKey(nameof(IdTopic))]
    public TopicModel Topic { get; set; } = null!;
    
    public string? Description { get; set; }

    public ICollection<SlideModel> Slides { get; set; } = new List<SlideModel>();
}

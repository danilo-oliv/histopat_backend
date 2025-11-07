using histopat_back.Dominio.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Topic;

public class Topic : BaseEntity<TopicHistory>
{

    public int IdModule { get; set; }

    [ForeignKey("IdModule")]
    public Module.Module Module { get; set; } = null!;

    public ICollection<Subtopic.Subtopic> SubTopics { get; set; } = new List<Subtopic.Subtopic>();
}

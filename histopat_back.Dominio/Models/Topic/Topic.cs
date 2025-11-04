using histopat_back.Dominio.Models.Base;

namespace histopat_back.Dominio.Models.Topic;

public class Topic : BaseEntity<TopicHistory>
{

    public int IdModule { get; set; }

    public Module.Module Module { get; set; } = null!;

    public ICollection<Subtopic.Subtopic> SubTopics { get; set; } = new List<Subtopic.Subtopic>();
}

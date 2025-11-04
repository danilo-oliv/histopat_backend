using histopat_back.Dominio.Models.Base;

namespace histopat_back.Dominio.Models.Topic;

public class TopicHistory : BaseHistory
{
    public int IdTopic { get; set; }

    public Topic Topic { get; set; } = null!;

}

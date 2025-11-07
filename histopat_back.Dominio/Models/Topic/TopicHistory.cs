using histopat_back.Dominio.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Topic;

public class TopicHistory : BaseHistory
{
    public int IdTopic { get; set; }

    [ForeignKey("IdTopic")]
    public Topic Topic { get; set; } = null!;

}

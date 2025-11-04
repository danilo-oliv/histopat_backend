using histopat_back.Dominio.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Topic;

[Table("TopicHistory")]
public class TopicHistory : BaseHistory
{
    public int IdTopic { get; set; }

    [ForeignKey(nameof(IdTopic))]
    public TopicModel Topic { get; set; } = null!;

}

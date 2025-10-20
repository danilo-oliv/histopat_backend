using histopat_back.Models.Base;
using histopat_back.Models.Module;
using histopat_back.Models.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Topic;

[Table("TopicHistory")]
public class TopicHistory : BaseHistory
{
    public int IdTopic { get; set; }

    [ForeignKey(nameof(IdTopic))]
    public TopicModel Topic { get; set; } = null!;

}

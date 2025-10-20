using histopat_back.Models.Base;
using histopat_back.Models.Module;
using histopat_back.Models.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Subtopic;

[Table("SubTopicHistory")]
public class SubTopicHistory : BaseHistory
{    public int IdSubTopic { get; set; }

    [ForeignKey(nameof(IdSubTopic))]
    public SubtopicModel Subtopic { get; set; } = null!;
}

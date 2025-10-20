using histopat_back.Models.Base;
using histopat_back.Models.Module;
using histopat_back.Models.Subtopic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Topic;

[Table("Topic")]
public class TopicModel : BaseEntity<TopicHistory>
{

    public int IdModule { get; set; }

    [ForeignKey(nameof(IdModule))]
    public ModuleModel Module { get; set; } = null!;

    public ICollection<SubtopicModel> SubTopics { get; set; } = new List<SubtopicModel>();
}

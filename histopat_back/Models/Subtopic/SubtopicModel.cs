using histopat_back.Models.Base;
using histopat_back.Models.Slide;
using histopat_back.Models.Topic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Subtopic;

[Table("SubTopic")]
public class SubtopicModel : BaseEntity<SubTopicHistory>
{
   
    public int IdTopic { get; set; }

    [ForeignKey(nameof(IdTopic))]
    public TopicModel Topic { get; set; } = null!;

    public ICollection<SubTopicHistory> SubTopicHistories { get; set; } = new List<SubTopicHistory>();
    public ICollection<SlideModel> Slides { get; set; } = new List<SlideModel>();
}

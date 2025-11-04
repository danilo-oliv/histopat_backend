
using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Module;
using histopat_back.Dominio.Models.Subtopic;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Topic;

[Table("Topic")]
public class TopicModel : BaseEntity<TopicHistory>
{

    public int IdModule { get; set; }

    [ForeignKey(nameof(IdModule))]
    public ModuleModel Module { get; set; } = null!;

    public ICollection<SubtopicModel> SubTopics { get; set; } = new List<SubtopicModel>();
}

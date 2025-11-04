using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Topic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Module;

[Table("Module")]
public class ModuleModel : BaseEntity<ModuleHistory>
{
    public ICollection<TopicModel> Topics { get; set; } = new List<TopicModel>();

    [Required]
    public string ImageUrl { get; set; } = null!;

    public string? Description { get; set; } = string.Empty;
}

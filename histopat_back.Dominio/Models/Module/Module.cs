using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Topic;

namespace histopat_back.Dominio.Models.Module;

public class Module : BaseEntity<ModuleHistory>
{
    public ICollection<Topic.Topic> Topics { get; set; } = new List<Topic.Topic>();

    public string ImageUrl { get; set; } = null!;

    public string? Description { get; set; } = string.Empty;
}

using histopat_back.Dominio.Models.Module;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Subtopic;
using histopat_back.Dominio.Models.Topic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.User;

[Table("User")]
public class UserModel
{
    [Key]
    public int IdUser { get; set; }

    [Required, StringLength(150)]
    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; }

    // Navigation
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<ModuleHistory> ModuleHistories { get; set; } = new List<ModuleHistory>();
    public ICollection<TopicHistory> TopicHistories { get; set; } = new List<TopicHistory>();
    public ICollection<SubTopicHistory> SubTopicHistories { get; set; } = new List<SubTopicHistory>();
    public ICollection<SlideHistory> SlideHistories { get; set; } = new List<SlideHistory>();
}

using histopat_back.Models.Base;
using histopat_back.Models.Topic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Module;

[Table("Module")]
public class ModuleModel : BaseEntity<ModuleHistory>
{
    public ICollection<TopicModel> Topics { get; set; } = new List<TopicModel>();
    public ICollection<ModuleImage> ModuleImages { get; set; } = new List<ModuleImage>();
    
    [Required]
    public string Description { get; set; } = string.Empty;
}

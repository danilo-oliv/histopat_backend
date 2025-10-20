using histopat_back.Models.Base;
using histopat_back.Models.Subtopic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Slide;

[Table("Slide")]
public class SlideModel : BaseEntity<SlideHistory>
{
  
    public int IdSubTopico { get; set; }


    [Required]
    public string Image { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;


    [ForeignKey(nameof(IdSubTopico))]
    public SubtopicModel SubTopic { get; set; } = null!;

}

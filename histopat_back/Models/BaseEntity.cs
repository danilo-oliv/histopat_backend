using System.ComponentModel.DataAnnotations;

namespace histopat_back.Models
{
    public abstract class BaseEntity
    {
        [Key]
        int Id { get; set; }
        [Required]
        string Title { get; set; }
        bool Active { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? LastModified { get; set; }
    }
}

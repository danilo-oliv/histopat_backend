using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.User;

[Table("Roles")]
public class Role
{
    [Key]
    public byte IdRoles { get; set; }

    public bool Active { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    // Navigation
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

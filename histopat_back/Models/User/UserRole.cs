using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.User;

[Table("UserRole")]
public class UserRole
{
    // Chave composta (IdUser + IdRoles) — configure no DbContext via Fluent API
    [Key]
    public int IdUserRole { get; set; }
    public int IdUser { get; set; }
    public byte IdRoles { get; set; }

    public bool Active { get; set; }

    [ForeignKey(nameof(IdUser))]
    public UserModel User { get; set; } = null!;

    [ForeignKey(nameof(IdRoles))]
    public Role Role { get; set; } = null!;
}

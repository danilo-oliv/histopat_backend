using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models;

[Table("UserRole")]
public class UserRole
{
    // Chave composta (IdUser + IdRoles) — configure no DbContext via Fluent API
    public long IdUser { get; set; }
    public byte IdRoles { get; set; }

    public bool Active { get; set; }

    [ForeignKey(nameof(IdUser))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(IdRoles))]
    public Role Role { get; set; } = null!;
}

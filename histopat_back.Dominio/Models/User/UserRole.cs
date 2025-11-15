using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.User;

public class UserRole
{
    public int IdUser { get; set; }
    public byte IdRole { get; set; }

    public bool Active { get; set; }

    public User User { get; set; } = null!;

    public Role Role { get; set; } = null!;
}

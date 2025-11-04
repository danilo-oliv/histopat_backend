namespace histopat_back.Dominio.Models.User;

public class Role
{
    public byte IdRole { get; set; }

    public bool Active { get; set; }

    public string Name { get; set; } = string.Empty;

    // Navigation
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

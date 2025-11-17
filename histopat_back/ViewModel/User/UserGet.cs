using histopat_back.ViewModel.Role;
namespace histopat_back.ViewModel.User
{
    public class UserGet
    {
        public int IdUser { get; set; }

        public string Name { get; set; } = null!;

        public bool Active { get; set; }

        public ICollection<RoleGet> Roles { get; set; } = new List<RoleGet>();
    }
}

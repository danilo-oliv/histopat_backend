using histopat_back.ViewModel.Role;

namespace histopat_back.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<RoleGet> findAllRoles();
    }
}

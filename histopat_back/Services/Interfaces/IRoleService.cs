using histopat_back.ViewModel.Role;

namespace histopat_back.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleGet>> FindAllRoles();
        Task<RoleGet> FindById(byte roleId);
        Task SaveRole(RolePost rolePost);
        Task EditRole(RoleEdit roleEdit, byte roleId);
        Task DeleteRole(byte roleId);
    }
}

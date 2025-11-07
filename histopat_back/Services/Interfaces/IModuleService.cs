using histopat_back.ViewModel.Module;

namespace histopat_back.Services.Interfaces
{
    public interface IModuleService
    {
        public Task<ICollection<ModuleGet>> FindAllModulesAsync();
        public Task<ModuleGet> FindModuleByIdAsync(int moduleId);
        public Task SaveModuleAsync(ModulePost modulePost);
        public Task EditModuleAsync(ModuleEdit moduleEdit, int moduleId);
        public Task DeleteModuleAsync(int moduleId);
    }
}

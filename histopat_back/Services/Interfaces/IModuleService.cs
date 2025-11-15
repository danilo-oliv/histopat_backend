using histopat_back.ViewModel.Module;

namespace histopat_back.Services.Interfaces
{
    public interface IModuleService
    {
        public Task<IEnumerable<ModuleGet>> FindAllModules();

        public Task<ModuleGet> FindById(int moduleId);
        public Task SaveModule(ModulePost modulePost);
        public Task EditModule(ModuleEdit moduleEdit, int moduleId);
        public Task DeleteModule(int moduleId);
    }
}

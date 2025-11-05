using histopat_back.ViewModel.Module;
using histopat_back.ViewModel.Slide;

namespace histopat_back.Services.Interfaces
{
    public interface IModuleService
    {
        public Task<ModuleGet> FindAllModulesById(int moduleId);
        public void SaveModule(ModulePost modulePost);
        public void EditModule(ModulePost moduleEdit);
        public void DeleteModule(int moduleId);
    }
}

using histopat_back.Context;
using histopat_back.Dominio.Models.Module;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Module;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class ModuleService : IModuleService
    {
        private readonly HistopatDbContext _dbContext;
        private readonly IMapper _mapper;

        public ModuleService(HistopatDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<ModuleGet>> FindAllModulesAsync()
        {
            try
            {
                var modules = await _dbContext.Modules
                    .AsNoTracking()
                    .Where(m => m.Active == true)
                    .Select(m => _mapper.Map<ModuleGet>(m))
                    .ToListAsync();

                return modules;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar módulos: {ex}");
            }
        }

        public async Task<ModuleGet> FindModuleByIdAsync(int moduleId)
        {
            try
            {
                var module = await _dbContext.Modules
                    .AsNoTracking()
                    .Where(m => m.Id == moduleId && m.Active == true)
                    .FirstOrDefaultAsync();

                if (module == null)
                    throw new Exception($"Não foi encontrado módulo com o id {moduleId}");

                return _mapper.Map<ModuleGet>(module);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o módulo: {ex}");
            }
        }

        public async Task SaveModuleAsync(ModulePost modulePost)
        {
            try
            {
                var moduleEntity = _mapper.Map<Module>(modulePost);

                await _dbContext.Modules.AddAsync(moduleEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar módulo: {ex}");
            }
        }

        public async Task EditModuleAsync(ModuleEdit moduleEdit, int moduleId)
        {
            try
            {
                var moduleDb = await _dbContext.Modules
                    .Where(m => m.Id == moduleId)
                    .FirstOrDefaultAsync();

                if (moduleDb == null)
                    throw new Exception($"Não foi encontrado módulo com o id {moduleId}");

                if (moduleEdit.Title != null)
                    moduleDb.Title = moduleEdit.Title;

                if (moduleEdit.Description != null)
                    moduleDb.Description = moduleEdit.Description;

                moduleDb.LastModified = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao editar o módulo: {ex}");
            }
        }

        public async Task DeleteModuleAsync(int moduleId)
        {
            try
            {
                var moduleDb = await _dbContext.Modules
                    .Where(m => m.Id == moduleId)
                    .FirstOrDefaultAsync();

                if (moduleDb == null)
                    throw new Exception($"Não foi encontrado módulo com o id {moduleId}");

                moduleDb.Active = false;
                moduleDb.LastModified = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir o módulo: {ex}");
            }
        }
    }
}

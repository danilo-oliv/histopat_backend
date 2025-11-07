using histopat_back.Context;
using histopat_back.Dominio.Models.Module;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Module;
using histopat_back.ViewModel.Topic;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class ModuleService : IModuleService
    {
        private HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ModuleService> _logger;
        public ModuleService(HistopatDbContext dbContext, IMapper mapper, ILogger<ModuleService> logger)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<IEnumerable<ModuleGet>> FindAllModules()
        {
            var modules = await _dbContext.Modules.AsNoTracking().Where(m => m.Active == true).Include(m => m.Topics).ToListAsync();

            var mappedModules = _mapper.Map<IEnumerable<ModuleGet>>(modules);

            return mappedModules;
        }

        public async Task<ModuleGet> FindById(int moduleId)
        {
            var module = await _dbContext.Modules.AsNoTracking().Where(m => m.Id == moduleId && m.Active == true).Select(m => _mapper.Map<ModuleGet>(m)).FirstOrDefaultAsync();

            if (module == null) throw new Exception(message: $"Não foi encontrado módulo com o id {moduleId}");

            return module;
        }

        public async Task SaveModule(ModulePost modulePost)
        {
            var moduleEntity = _mapper.Map<Module>(modulePost);

            await _dbContext.Modules.AddAsync(moduleEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditModule(ModuleEdit moduleEdit, int moduleId)
        {
            var moduleDb = await _dbContext.Modules.Where(m => m.Id == moduleId).FirstOrDefaultAsync();

            if (moduleDb == null) throw new Exception(message: $"Não foi encontrado módulo com o id {moduleId}");

            if (moduleEdit.Title != null)
            {
                moduleDb.Title = moduleEdit.Title;
            }
            if (moduleEdit.Description != null)
            {
                moduleDb.Description = moduleEdit.Description;
            }
            if (moduleEdit.ImageUrl != null)
            {
                moduleDb.ImageUrl = moduleEdit.ImageUrl;
            }

            moduleDb.LastModified = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteModule(int moduleId)
        {
            var moduleDb = await _dbContext.Modules.Where(m => m.Id == moduleId).FirstOrDefaultAsync();

            if (moduleDb == null) throw new Exception(message: $"Não foi encontrado módulo com o id {moduleId}");

            moduleDb.Active = false;
            moduleDb.LastModified = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }
    }
}

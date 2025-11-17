using histopat_back.Context;
using histopat_back.Dominio.Models.User;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Role;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class RoleService : IRoleService
    {
        private readonly HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(HistopatDbContext dbContext, IMapper mapper, ILogger<RoleService> logger)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<IEnumerable<RoleGet>> FindAllRoles()
        {
            var roles = await _dbContext.Roles
                .Where(r => r.Active)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoleGet>>(roles);
        }

        public async Task<RoleGet> FindById(byte roleId)
        {
            var role = await _dbContext.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.IdRole == roleId && r.Active);

            if (role == null)
                throw new Exception($"Não foi encontrada função com o id {roleId}");

            return _mapper.Map<RoleGet>(role);
        }

        public async Task SaveRole(RolePost rolePost)
        {
            try
            {
                var role = _mapper.Map<Role>(rolePost);

                await _dbContext.Roles.AddAsync(role);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar Role");

                throw new Exception(
                    $"Erro ao salvar Role: {ex.InnerException?.Message ?? ex.Message}"
                );
            }
        }


        public async Task EditRole(RoleEdit roleEdit, byte roleId)
        {
            try
            {
                
                var roleDb = await _dbContext.Roles.FirstOrDefaultAsync(r => r.IdRole == roleId);

                if (roleDb == null)
                    throw new Exception($"Não foi encontrada função com o id {roleId}");

                if (roleEdit.Name != null)
                    roleDb.Name = roleEdit.Name;

                if (roleEdit.Active.HasValue)
                    roleDb.Active = roleEdit.Active.Value;

                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar Role");
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task DeleteRole(byte roleId)
        {
            var roleDb = await _dbContext.Roles.FirstOrDefaultAsync(r => r.IdRole == roleId);

            if (roleDb == null)
                throw new Exception($"Não foi encontrada função com o id {roleId}");

            roleDb.Active = false;

            await _dbContext.SaveChangesAsync();
        }
    }
}

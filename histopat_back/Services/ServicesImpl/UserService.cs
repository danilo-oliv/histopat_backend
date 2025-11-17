using histopat_back.Context;
using histopat_back.Dominio.Models.User;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Role;
using histopat_back.ViewModel.User;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class UserService : IUserService
    {
        private readonly HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(HistopatDbContext dbContext, IMapper mapper, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserGet>> FindAllUsers()
        {
            var result = await _dbContext.Users
                .Where(u => u.Active)
                .AsNoTracking()
                .Select(u => new UserGet
                {
                    IdUser = u.IdUser,
                    Name = u.Name,
                    Active = u.Active,
                    Roles = u.UserRoles
                        .Where(ur => ur.Active && ur.Role.Active)
                        .Select(ur => new RoleGet
                        {
                            IdRole = ur.Role.IdRole,
                            Name = ur.Role.Name,
                            Active = ur.Role.Active
                        }).ToList()
                })
                .ToListAsync();

            return result;
        }

        public async Task<UserGet> FindById(int userId)
        {
            var result = await _dbContext.Users
                .Where(u => u.IdUser == userId && u.Active)
                .AsNoTracking()
                .Select(u => new UserGet
                {
                    IdUser = u.IdUser,
                    Name = u.Name,
                    Active = u.Active,
                    Roles = u.UserRoles
                        .Where(ur => ur.Active && ur.Role.Active)
                        .Select(ur => new RoleGet
                        {
                            IdRole = ur.Role.IdRole,
                            Name = ur.Role.Name,
                            Active = ur.Role.Active
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new Exception($"Não foi encontrado usuário ativo com o id {userId}");
            }

            return result;
        }

        public async Task SaveUser(UserPost userPost)
        {
            var user = _mapper.Map<User>(userPost);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            foreach (var roleId in userPost.Roles)
            {
                await _dbContext.UserRoles.AddAsync(new UserRole
                {
                    IdUser = user.IdUser,
                    IdRole = roleId,
                    Active = true
                });
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditUser(UserEdit userEdit, int userId)
        {
            var userDb = await _dbContext.Users.Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.IdUser == userId);

            if (userDb == null)
                throw new Exception($"Não foi encontrado usuário com o id {userId}");

            if (userEdit.Name != null)
                userDb.Name = userEdit.Name;

            if (userEdit.Active.HasValue)
                userDb.Active = userEdit.Active.Value;

            if (userEdit.Roles != null)
            {
                userDb.UserRoles.Clear();
                foreach (var roleId in userEdit.Roles)
                {
                    userDb.UserRoles.Add(new UserRole
                    {
                        IdUser = userDb.IdUser,
                        IdRole = roleId,
                        Active = true
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var userDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.IdUser == userId);

            if (userDb == null)
                throw new Exception($"Não foi encontrado usuário com o id {userId}");

            userDb.Active = false;

            await _dbContext.SaveChangesAsync();
        }
    }
}

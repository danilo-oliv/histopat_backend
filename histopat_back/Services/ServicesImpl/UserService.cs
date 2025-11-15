using histopat_back.Context;
using histopat_back.Dominio.Models.User;
using histopat_back.Services.Interfaces;
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
            var users = await _dbContext.Users
                .Include(u => u.UserRoles.Where(ur => ur.Active))
                .ThenInclude(ur => ur.Role)
                .Where(u => u.Active)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserGet>>(users);
        }

        public async Task<UserGet> FindById(int userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserRoles.Where(ur => ur.Active))
                .ThenInclude(ur => ur.Role)
                .Where(u => u.IdUser == userId && u.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception($"Não foi encontrado usuário com o id {userId}");

            return _mapper.Map<UserGet>(user);
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

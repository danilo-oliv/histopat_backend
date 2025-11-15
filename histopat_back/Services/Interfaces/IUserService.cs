using histopat_back.ViewModel.User;

namespace histopat_back.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserGet>> FindAllUsers();
        Task<UserGet> FindById(int userId);
        Task SaveUser(UserPost userPost);
        Task EditUser(UserEdit userEdit, int userId);
        Task DeleteUser(int userId);
    }
}

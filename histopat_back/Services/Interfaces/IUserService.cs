using histopat_back.ViewModel.User;

namespace histopat_back.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserGet> FindAllUsers();
    }
}

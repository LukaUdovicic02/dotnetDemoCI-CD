using Shared.Models;

namespace CI_CD_testv3.Services
{
    public interface IAuthService
    {
        Task<User> ValidateUser(string username, string password);
        Task RegisterUser(User user);
    }
}
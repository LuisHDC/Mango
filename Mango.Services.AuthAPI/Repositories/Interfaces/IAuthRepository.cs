using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetApplicationUserByEmailAsync(string email);
        Task<ApplicationUser> GetApplicationUserByUserNameAsync(string userName);
    }
}

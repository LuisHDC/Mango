using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuthRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<ApplicationUser> GetApplicationUserByEmailAsync(string email) => await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
        public async Task<ApplicationUser> GetApplicationUserByUserNameAsync(string userName) => await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
    }
}

using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Repositories.Interfaces;
using Mango.Services.AuthAPI.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IAuthRepository authRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Register(RegistrationRequestDTO requestDTO)
        {
            var user = new ApplicationUser
            {
                UserName = requestDTO.Email,
                Email = requestDTO.Email,
                NormalizedEmail = requestDTO.Email.ToUpper(),
                Name = requestDTO.Name,
                PhoneNumber = requestDTO.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, requestDTO.Password);

                if (result.Succeeded)
                {
                    var userToReturn = await _authRepository.GetApplicationUserByEmailAsync(requestDTO.Email);

                    var userDto = new UserDTO
                    {
                        Email = requestDTO.Email,
                        ID = userToReturn.Id,
                        Name = requestDTO.Name,
                        PhoneNumber = requestDTO.PhoneNumber,
                    };

                    return userDto;
                }
            }
            catch (Exception ex)
            {
            }

            return new UserDTO();
        }
    }
}

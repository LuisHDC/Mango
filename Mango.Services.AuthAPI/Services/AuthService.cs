using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Repositories.Interfaces;
using Mango.Services.AuthAPI.Service.Interfaces;
using Mango.Services.AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IAuthRepository authRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _authRepository.GetApplicationUserByEmailAsync(email);

            if (user != null)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

                await _userManager.AddToRoleAsync(user, roleName);

                return true;
            }

            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO)
        {
            var user = await _authRepository.GetApplicationUserByUserNameAsync(requestDTO.UserName);

            var isValid = await _userManager.CheckPasswordAsync(user, requestDTO.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDTO();
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            var userDTO = new UserDTO
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };

            var loginReponse = new LoginResponseDTO()
            {
                User = userDTO,
                Token = token,
            };

            return loginReponse;
        }

        public async Task<string> Register(RegistrationRequestDTO requestDTO)
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

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return "Error encountered";
            }
        }
    }
}

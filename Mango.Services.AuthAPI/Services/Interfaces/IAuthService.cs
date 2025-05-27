using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO requestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO);
    }
}

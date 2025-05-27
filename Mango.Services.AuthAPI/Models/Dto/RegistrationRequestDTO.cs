namespace Mango.Services.AuthAPI.Models.Dto
{
    public class RegistrationRequestDTO
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}

using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserInterface _userInterface;
        private readonly IJWTSettingsService _jWTSettingsService;
        public UserService(IUserInterface userInterface, IJWTSettingsService jWTSettingsService)
        {
            _userInterface = userInterface;
            _jWTSettingsService = jWTSettingsService;
        }
        public async Task<Response<UserResponseModel>> Login(LoginRequestModel model)
        {
            var user = await _userInterface.GetUserAsync(m => m.Email == model.Email);
            if (user == null)
            {
                return new Response<UserResponseModel>
                {
                    Message = "User not found",
                    Status = false,
                };
            }
            var password = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (password)
            {
                var token = _jWTSettingsService.GenerateToken(user);
                return new Response<UserResponseModel>
                {
                    Message = "Login Successful",
                    Status = true,
                    Value = new UserResponseModel
                    {
                       Id = user.Id,
                        Email = user.Email,
                        Token = token,
                        Roles = user.UserRoles.Select(x => x.Role.Name).ToList(),
                    }
                };

            }
            return new Response<UserResponseModel>
            {
                Message = "Invalid Credentials",
                Status = false,
            };
        }
    }
}
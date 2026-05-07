using WeTicket.Data.DTOs;
using WeTicket.Data.Models;

namespace WeTicket.Services.IService
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);

        Task<UpdateProfileDto> GetUserProfileAsync(string userId);
        Task<AuthModel> UpdateUserProfileAsync(string userId, UpdateProfileDto model);
        
    }
}

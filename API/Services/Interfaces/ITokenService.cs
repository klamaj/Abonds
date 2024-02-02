using Core.Models;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserModel user);
    }
}
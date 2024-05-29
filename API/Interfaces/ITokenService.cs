using API.Models;

namespace API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserModel user);
    }
}
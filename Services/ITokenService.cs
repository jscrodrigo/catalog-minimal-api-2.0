using Catalog.Models;

namespace Catalog.Services
{
    public interface ITokenService
    {
        string GenerateToken(string key, string issuer, string audience, UserModel user);
    }
}

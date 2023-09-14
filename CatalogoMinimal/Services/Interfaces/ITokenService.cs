using CatalogoMinimal.Models;

namespace CatalogoMinimal.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string key, string issuer, string audience, User user);
    }
}

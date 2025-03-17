using Infrastructure.Persistence.Identity;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user);
    }
}
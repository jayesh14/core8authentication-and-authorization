using System.Security.Claims;

namespace UserManagement.Services
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}

using System.Security.Claims;

namespace Labo.API.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claims)
        {
            string? value =  claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return value is not null ? new Guid(value) : Guid.Empty;
        }

        public static string GetEmail(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }
    }
}

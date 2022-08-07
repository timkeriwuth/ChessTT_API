using System.Security.Claims;

namespace Labo.API.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claims)
        {
            string? value =  claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return new Guid(value ?? string.Empty);
        }
    }
}

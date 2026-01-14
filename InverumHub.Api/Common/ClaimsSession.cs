using InverumHub.Core.Common;
using System.Security.Claims;

namespace InverumHub.Api.Common
{
    public static class ClaimsSession
    {
        public static GlobalSessionModel ToGlobalSession(this ClaimsPrincipal user)
        {
            return new GlobalSessionModel
            {
                UserId = Guid.Parse(
                    user.FindFirstValue(ClaimTypes.NameIdentifier)
                ),
                UserName = user.FindFirstValue(ClaimTypes.Name),
                UserEmail = user.FindFirstValue(ClaimTypes.Email),
                ApplicationName = user.FindFirst("app")?.Value ?? string.Empty,
                RoleNames = user.FindAll(ClaimTypes.Role)
                                .Select(c => c.Value)
                                .ToList()
            };
        }
    }
}

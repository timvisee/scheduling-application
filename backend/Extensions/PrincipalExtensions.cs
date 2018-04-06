using System.Linq;
using System.Security.Principal;

namespace backend.Extensions
{
    public static class PrincipalExtensions
    {
        public static bool IsInAnyRoles(this IPrincipal principal, params string[] roles)
        {
            return roles.Any(principal.IsInRole);
        }
    }
}

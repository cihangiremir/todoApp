using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimsType)//Claimleri getir.
        {
            var result = claimsPrincipal?.FindAll(claimsType)?.Select(t => t.Value).ToList();
            return result;
        }
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)//Rolleri getir.
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
        public static long GetId(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.FindAll("userId")?.Select(x => x.Value).SingleOrDefault();
            return Convert.ToInt64(result);
        }
    }
}

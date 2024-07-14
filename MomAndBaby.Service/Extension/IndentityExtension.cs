using Microsoft.AspNetCore.Http;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace MomAndBaby.Service.Extension
{
    public static class IndentityExtension
    {
        //get, extract userId in jwt token 
        public static string GetUserIdFromToken(this IPrincipal user)
        {
            if (user == null)
                return string.Empty;

            var identity = user.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity!.Claims;
            return claims.FirstOrDefault(s => s.Type.Equals(UserClaimType.UserId))?.Value ?? string.Empty;
        }

        public static int GetUserRoleFromToken(this IPrincipal user)
        {
            if (user == null)
                return -1;

            var identity = user.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity!.Claims;

            var result = claims.FirstOrDefault(s => s.Type.Equals(UserClaimType.Role))?.Value ?? "-1";

            return int.Parse(result);
        }

    }
}

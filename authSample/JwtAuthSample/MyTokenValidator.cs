using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthSample
{
    public class MyTokenValidator : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {

            validatedToken = null;
            var identity=new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            if (securityToken == "abcdef")
            {
                identity.AddClaim(new Claim("name","我零0七"));
                identity.AddClaim(new Claim("SuperAdminOnly", "true"));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType,"admin"));
            }
            else
            {
                return null;
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }
    }
}

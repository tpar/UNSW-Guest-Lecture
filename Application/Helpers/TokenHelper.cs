using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.Helpers
{
    public static class TokenHelper
    {
        public static string ExtractAuth0_Id(string idToken)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            string sub = token.Claims.ToList().Where(x => x.Type == "sub").FirstOrDefault().Value;
            string auth0_id = sub.Replace("auth0|", "");
            return auth0_id;
        }
    }
}

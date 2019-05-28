using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;

namespace AuthenticationService.Service
{
    public class TokenService: ITokenService
    {
        public string GetJWTToken(string id)
        {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.UniqueName,id),
               new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString())
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_keepnote_authentication_key"));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "AuthenticationService",
                    audience: "KeepNoteServiceApi",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(5),
                    signingCredentials: credential
                );

            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}

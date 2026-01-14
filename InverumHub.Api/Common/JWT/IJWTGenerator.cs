using InverumHub.Core.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Api.Common.JWT
{
    public interface IJWTGenerator
    {
        string GenerateToken(GlobalSessionModel model);
    }

    public class JWTGenerator : IJWTGenerator
    {
        private readonly JWTConfig _jwtconfig;

        public JWTGenerator(IOptions<JWTConfig> jwtConfig)
        {
            _jwtconfig = jwtConfig.Value ?? throw new ArgumentNullException(nameof(jwtConfig));
        }

        public string GenerateToken(GlobalSessionModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var privateKeyPem = Encoding.UTF8.GetString(
                Convert.FromBase64String(_jwtconfig.SecretKeyBase64)
            );

            var rsa = RSA.Create();
            rsa.ImportFromPem(privateKeyPem.ToCharArray());

            var signingKey = new RsaSecurityKey(rsa)
            {
                KeyId = _jwtconfig.KeyId 
            };

            var now = DateTime.UtcNow;

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Email, model.UserEmail),
                        new Claim("app", model.ApplicationName)
                    };
            
            foreach (var role in model.RoleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = now,
                Expires = DateTime.UtcNow.AddMinutes(_jwtconfig.ExpirationMinutes),
                SigningCredentials = new SigningCredentials(
                    signingKey,
                    SecurityAlgorithms.RsaSha256
                ),
                Issuer = _jwtconfig.Issuer,
                Audience = _jwtconfig.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        
    }
}

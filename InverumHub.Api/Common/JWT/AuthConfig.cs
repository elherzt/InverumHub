using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Api.Common.JWT
{
    public static class AuthConfig
    {
        public static void ConfigureJwt(IServiceCollection services, IConfiguration config)
        {

            var jwtConfig = config.GetSection("JWTConfig").Get<JWTConfig>()!;

            var publicKeyPath = Path.Combine(
                AppContext.BaseDirectory,
                "keys",
                "ssot-public.key"
            );

            if (!File.Exists(publicKeyPath))
                throw new FileNotFoundException("JWT public key not found", publicKeyPath);

            var publicKeyPem = File.ReadAllText(publicKeyPath);

            var rsa = RSA.Create();
            rsa.ImportFromPem(publicKeyPem.ToCharArray());

            var rsaKey = new RsaSecurityKey(rsa)
            {
                KeyId = jwtConfig.KeyId
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,

                    IssuerSigningKey = rsaKey,

                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });
        }
    }
}

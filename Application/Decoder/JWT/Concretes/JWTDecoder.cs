using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Domain.ObjectValue;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Application.Decoders.JWT
{
    public class JWTDecoder : IJWTDecoder
    {
        public JWTBody Decoder(string jwt)
        {
            string? secretKey = Environment.GetEnvironmentVariable("SECRET_KEY_JWT");
            string? issuer = Environment.GetEnvironmentVariable("ISSUER_JWT");
            string? audience = Environment.GetEnvironmentVariable("AUDIENCE_JWT");

            if (secretKey is null)
                throw new InvalidOperationException("Secret key was not initialized.");

            if (issuer is null)
                throw new InvalidOperationException("Issuer was not initialized.");

            if (audience is null)
                throw new InvalidOperationException("Audience was not initialized.");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var handler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal claims = handler.ValidateToken(jwt, credentials, out _);

                foreach (var claim in claims.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");

                }
                var userIdClaim = claims.FindFirst("UserId")?.Value;
                var roleIdClaim = claims.FindFirst("RoleId")?.Value;
                var tenantIdClaim = claims.FindFirst("TenantId")?.Value; 

                if (userIdClaim == null || roleIdClaim == null || tenantIdClaim == null)
                    throw new InvalidOperationException("Missing required claims in JWT.");

                return new JWTBody
                {
                    UserId = Guid.Parse(userIdClaim),
                    RoleId = int.Parse(roleIdClaim),
                    TenantId = Guid.Parse(tenantIdClaim)
                };
            }
            catch (SecurityTokenException exception)
            {
                throw new InvalidOperationException(exception.Message);
            }
        }
    }
}

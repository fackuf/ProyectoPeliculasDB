using DisneyAlkemy.Data.DataModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DisneyAlkemy.Helpers
{
    public static class JwtHelper
    {
        public static IEnumerable<Claim> GetClaims(this UserToken userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name,userAccounts.UserName),
                new Claim(ClaimTypes.Email,userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier,userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Expiration,DateTime.UtcNow.AddDays(1).ToString("MM ddd dd yyyy HH:mm:ss tt"))

            };
            if (userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));
            }
            return claims;

        }

        public static IEnumerable<Claim> GetClaims(this UserToken userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserToken GenTokenKey(UserToken model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserToken();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                // Obtener clave secreta
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id;
                // Expira en 1 dia
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                // Validez
                userToken.Validity = expireTime.TimeOfDay;
                // Generar jw token
                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256));

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;
                return userToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar jwt", ex);
            }
        }
    }
}

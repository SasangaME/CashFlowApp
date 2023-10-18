namespace CashFlowApp.Utils.Security;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtUtil
{
    public static string CreateToken(string key, string username, string role, string name, string issuer,
        string audience)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.GivenName, name),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims,
            expires: DateTime.Now.AddMinutes(150), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string? ValidateToken(string token, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userName = jwtToken.Claims.First(q => q.Type == ClaimTypes.NameIdentifier).Value;
            return userName;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
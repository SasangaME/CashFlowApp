namespace CashFlowApp.Utils.Security;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtUtil
{
    public static string CreateToken(string key, string username, int userId, string issuer,
        string audience)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims,
            expires: DateTime.Now.AddMinutes(150), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static JwtInfo? ValidateToken(string token, string secret)
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
            var userName = jwtToken.Claims.First(q => q.Type == ClaimTypes.Name).Value;
            var userId = jwtToken.Claims.First(q => q.Type == ClaimTypes.NameIdentifier).Value;
            JwtInfo jwtInfo = new() { Username = userName, UserId = Convert.ToInt32(userId)};
            return jwtInfo;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}

public class JwtInfo
{
    public string Username { get; set; } = string.Empty;
    public int UserId { get; set; } 
}
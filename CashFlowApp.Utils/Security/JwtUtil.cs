namespace CashFlowApp.Utils.Security;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtUtil
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
}
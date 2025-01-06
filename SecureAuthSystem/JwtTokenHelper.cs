using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtTokenHelper
{
    public static string GenerateToken(string email, string role, string secretKey)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        if (key.KeySize < 256)
        {
            throw new ArgumentException("The secret key must be at least 32 characters long.");
        }

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "YourAppIssuer",
            Audience = "YourAppAudience",
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

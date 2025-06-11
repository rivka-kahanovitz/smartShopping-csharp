// מחלקה ליצירת JWT Token
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
public static class TokenGenerator
{
    // גרסה קיימת – אל תשני!
    public static string GenerateToken(string email, string secretKey, string issuer, string audience)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.Email, email)
    };

        return GenerateToken(claims, secretKey, issuer, audience);
    }

    // גרסה חדשה – לקבלת Claims ישירות
    public static string GenerateToken(IEnumerable<Claim> claims, string secretKey, string issuer, string audience)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}



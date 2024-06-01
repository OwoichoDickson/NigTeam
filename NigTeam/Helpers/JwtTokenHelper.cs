using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenHelper
{
    private readonly IConfiguration _configuration;

    public JwtTokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string username, string role)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var keyValue = jwtSettings["Key"];
        var expiresInMinutes = jwtSettings["ExpiresInMinutes"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        // Check if required JWT settings are present
        if (string.IsNullOrEmpty(keyValue) || string.IsNullOrEmpty(expiresInMinutes) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
        {
            throw new ApplicationException("JWT settings are missing or incomplete in configuration.");
        }

        var key = Encoding.ASCII.GetBytes(keyValue);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role) // Add role claim
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(expiresInMinutes)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = issuer,
            Audience = audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

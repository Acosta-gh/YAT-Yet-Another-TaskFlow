using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services;

public class TokenService : ITokenService
{
    public string CrearToken(Usuario usuario, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email,  usuario.Email!),
        };

        foreach (var rol in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, rol));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!));
        
        var token = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES")!)
            ),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

}
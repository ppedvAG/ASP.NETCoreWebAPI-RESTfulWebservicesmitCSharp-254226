using BusinessModel.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessModel.Security;

public class JwtTokenService : ITokenService
{
    private readonly IOptions<JwtOptions> _options;

    private SymmetricSecurityKey _signingKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SigningKey));

    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _options = options;
    }

    public string CreateToken(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credentials,
            Issuer = _options.Value.Issuer,
            Audience = _options.Value.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
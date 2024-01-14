using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.API.Users.Models;
using Domain.Ports;
using Infra.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Host.Security;

public class JWT(JwtConfig jwtConfig, IClock clock)
{
  public string Generate(User user)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>(new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
    });
    claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));

    var token = new JwtSecurityToken(jwtConfig.Issuer,
      jwtConfig.Issuer,
      expires: clock.Now().AddDays(1),
      signingCredentials: credentials,
      claims: claims);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public Guid? Parse(string token)
  {
    try
    {
      var x = new JwtSecurityTokenHandler().ReadJwtToken(token);
      return Guid.Parse(x.Subject);
    }
    catch
    {
      return null;
    }
  }
}
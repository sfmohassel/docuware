using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.API.Users.Models;
using Infra.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Host.Security;

public static class JWT
{
  public static string Generate(User user, JwtConfig jwtConfig)
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
      expires: DateTime.Now.AddDays(1),
      signingCredentials: credentials,
      claims: claims);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public static Guid? Parse(string token)
  {
    try
    {
      var x = new JwtSecurityTokenHandler().ReadJwtToken(token);
      return Guid.Parse(x.Subject);
    }
    catch (Exception e)
    {
      return null;
    }
  }
}
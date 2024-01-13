using Domain.Ports.Users;

namespace Infra.Adapters;

public class PasswordHasher : IPasswordHasher
{
  public string Hash(string rawPassword)
  {
    return BCrypt.Net.BCrypt.HashPassword(rawPassword);
  }

  public bool Verify(string hashedPassword, string rawPassword)
  {
    return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword);
  }
}
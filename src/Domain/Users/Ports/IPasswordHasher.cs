namespace Domain.Users.Ports;

public interface IPasswordHasher
{
  Task<string> Hash(string rawPassword);

  Task<bool> Verify(string hashedPassword, string rawPassword);
}
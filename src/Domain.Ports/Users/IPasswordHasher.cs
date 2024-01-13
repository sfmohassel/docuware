namespace Domain.Ports.Users;

public interface IPasswordHasher
{
  Task<string> Hash(string rawPassword);

  Task<bool> Verify(string hashedPassword, string rawPassword);
}
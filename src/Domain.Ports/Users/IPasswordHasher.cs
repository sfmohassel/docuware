namespace Domain.Ports.Users;

public interface IPasswordHasher
{
  string Hash(string rawPassword);

  bool Verify(string hashedPassword, string rawPassword);
}
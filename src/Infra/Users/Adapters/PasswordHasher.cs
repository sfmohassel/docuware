﻿using Domain.Users.Ports;

namespace Infra.Users.Adapters;

public class PasswordHasher : IPasswordHasher
{
  public Task<string> Hash(string rawPassword)
  {
    return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(rawPassword));
  }

  public Task<bool> Verify(string hashedPassword, string rawPassword)
  {
    return Task.FromResult(BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword));
  }
}
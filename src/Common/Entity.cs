﻿using Common.Exceptions;

namespace Common;

public abstract class Entity
{
  public long Id { get; private set; }
  public Guid PublicId { get; set; }

  protected Entity()
  {
    PublicId = Guid.NewGuid();
  }

  private void Validate()
  {
    if (Id < 0)
    {
      throw new InvalidIdException();
    }
  }
}
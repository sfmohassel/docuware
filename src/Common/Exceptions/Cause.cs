namespace Common.Exceptions;

public enum Cause
{
  InvalidData = 400,
  IntegrityViolation = 409,
  SomethingNotFound = 404,
  Unknown = 500,
  NoAccess = 403
}
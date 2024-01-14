namespace Infra.Configuration;

public class JwtConfig
{
  public string Issuer { get; set; }
  public string Audiences { get; set; }
  public string Key { get; set; }

  public IList<string> GetAudiences()
  {
    return Audiences?.Split(",").Select(a => a.Trim()).ToList() ?? [];
  }
}
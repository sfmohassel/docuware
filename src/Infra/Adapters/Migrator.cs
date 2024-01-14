using DbUp;

namespace Infra.Adapters;

public static class Migrator
{
  public static int Migrate(string connectionString)
  {
    var result = DeployChanges.To
      .PostgresqlDatabase(connectionString)
      .WithScriptsAndCodeEmbeddedInAssembly(typeof(Migrator).Assembly)
      .LogToConsole()
      .Build()
      .PerformUpgrade();

    if (!result.Successful)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Error in migrations:");
      Console.WriteLine(result.Error);
      Console.ResetColor();
      return -1;
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Migration Successful!");
    Console.ResetColor();
    return 0;
  }
}
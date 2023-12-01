using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace EarthQuakeData;

public class Wrapper
{
    public static IConfiguration ConfigConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            // .SetBasePath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location), "..", "..", ".."))
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        Console.WriteLine("App context " + Path.GetDirectoryName(System.AppContext.BaseDirectory));
        return config;
    }
}
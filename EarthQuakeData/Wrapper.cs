using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace EarthQuakeData;

public class Wrapper
{
    public static IConfiguration ConfigConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location), "..", "..", ".."))
            .AddJsonFile("appsettings.json")
            .Build();

        return config;
    }
    
}
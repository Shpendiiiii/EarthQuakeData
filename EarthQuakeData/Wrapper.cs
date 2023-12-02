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
        
        return config;
    }
}
using Microsoft.Extensions.Configuration;

namespace EarthQuakeData;

public class Wrapper
{
    public static IConfiguration ConfigConfiguration()
    {
        
        string basePath = Directory.GetCurrentDirectory();
        string appSettingsPath = Path.Combine(basePath, "../../../appsettings.json");
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile(appSettingsPath)
            .Build();

        return config;
    }
    
}
using Microsoft.Extensions.Configuration;

namespace EarthQuakeData;

//Contains a utility method
public class ConfigurationClass
{
    //This method return an IConfiguration object that lets me access the appsettings.json file to read the base urls of the api
    public static IConfiguration ConfigConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        
        return config;
    }
}
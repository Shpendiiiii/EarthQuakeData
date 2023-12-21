using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class ReturnFromAlertLevelUsgs : ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;
    
    public ReturnFromAlertLevelUsgs(RestClient _httpClient, dynamic _dataConverter)
    {
        httpClient = _httpClient;
        dataConverter = _dataConverter;
    }
    public void Execute()
    {
        ReturnAll();
    }
    
    public void ReturnAll()
    {
        DataProvider firstUsgs = new UsgsApi(dataConverter, httpClient);
        Console.Write("Enter the alert level (red, green, yellow): ");
        string qualifier = Console.ReadLine().Trim().ToLower();
        JObject info = firstUsgs.GetDataByOtherQualifiers(qualifier);
        firstUsgs.FormatConversion(info);
    }
}
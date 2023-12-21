
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class ReturnLocationUsgs : ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;

    public ReturnLocationUsgs(RestClient _httpClient, dynamic _dataConverter)
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
        DataProvider usgsObj = new UsgsApi(dataConverter, httpClient);
        Console.Write("Enter longitude: ");
        string longitude = Console.ReadLine()!.Trim().ToLower();
        Console.Write("Enter latitude: ");
        string latitude = Console.ReadLine()!.Trim().ToLower();
        JObject info = usgsObj.GetDataByLocation(longitude, latitude);
        usgsObj.FormatConversion(info);
    }
}
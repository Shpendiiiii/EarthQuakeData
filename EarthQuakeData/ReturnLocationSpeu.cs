using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class ReturnLocationSpeu : ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;

    public ReturnLocationSpeu(RestClient _httpClient, dynamic _dataConverter)
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
        DataProvider speuObj = new SpeuApi(dataConverter, httpClient);
        Console.Write("Enter longitude: ");
        string longitude = Console.ReadLine()!.Trim().ToLower();
        Console.Write("Enter latitude: ");
        string latitude = Console.ReadLine()!.Trim().ToLower();
        JObject info = speuObj.GetDataByLocation(longitude, latitude);
        speuObj.FormatConversion(info);
    }
}
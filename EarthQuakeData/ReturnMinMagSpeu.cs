using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class ReturnMinMagSpeu : ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;
    
    public ReturnMinMagSpeu(RestClient _httpClient, dynamic _dataConverter)
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
        Console.Write("Enter minimum magnitude: ");
        string qualifier = Console.ReadLine()!.Trim().ToLower();
        JObject info = speuObj.GetDataByOtherQualifiers(qualifier);
        speuObj.FormatConversion(info);
    }
}
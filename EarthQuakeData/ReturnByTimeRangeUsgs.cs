using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class ReturnByTimeRangeUsgs : ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;

    public ReturnByTimeRangeUsgs(RestClient _httpClient, dynamic _dataConverter)
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
        Console.Write("Enter starting time format 2020-12-20 (yyyy-MM-dd): ");
        string startTime = Console.ReadLine()!.Trim().ToLower();
        Console.Write("Enter end time (yyyy-MM-dd): ");
        string endTime = Console.ReadLine()!.Trim().ToLower();
        JObject info = usgsObj.GetDataByTimeRange(startTime, endTime);
        usgsObj.FormatConversion(info);
    }
}
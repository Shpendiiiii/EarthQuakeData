using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class  ReturnByTimeRangeSpeu: ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;

    public ReturnByTimeRangeSpeu(RestClient _httpClient, dynamic _dataConverter)
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
        Console.Write("Enter starting time format 2020-12-20 (yyyy-MM-dd): ");
        string startTime = Console.ReadLine()!.Trim().ToLower();
        Console.Write("Enter end time (yyyy-MM-dd): ");
        string endTime = Console.ReadLine()!.Trim().ToLower();
        JObject info = speuObj.GetDataByTimeRange(startTime, endTime);
        speuObj.FormatConversion(info);
    }
}
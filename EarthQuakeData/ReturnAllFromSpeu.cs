using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class ReturnAllFromSpeu : ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;
    
    public ReturnAllFromSpeu(RestClient _httpClient, dynamic _dataConverter)
    {
        httpClient = _httpClient;
        dataConverter = _dataConverter;
    }

    public void Execute()
    {
        ReturnAll();
    }

    public JObject ReturnAll()
    {
        DataProvider speuObj = new SpeuApi(dataConverter, httpClient);
        JObject info = speuObj.GetMostRecentData();
        speuObj.FormatConversion(info);
        return info;
    }
    
}
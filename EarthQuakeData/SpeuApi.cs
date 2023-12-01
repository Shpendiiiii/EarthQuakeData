using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static EarthQuakeData.Utils;

namespace EarthQuakeData;

public sealed class SpeuApi : DataProvider
{
    public override string Url { get; init; }

    public SpeuApi(IDataConverter dataConverter, RestClient client)
    {
        Url = Wrapper.ConfigConfiguration()["url_base_paths:speu:base"] ?? string.Empty;
        HttpClient = client;
        DataConverter = dataConverter;
    }

    public override JObject GetMostRecentData()
    {
        string today = GenerateTodayYesterdayDate().Item1;
        string yesterday = GenerateTodayYesterdayDate().Item2;

        var req = new RestRequest(Url + $"query?format=json&start={yesterday}&end={today}");
        Console.WriteLine($"url: {Url + $"query?format=json&start={yesterday}&end={today}"}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");

        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
    }

    public override JObject GetDataByLocation(string longitude, string latitude)
    {
        var req = new RestRequest(Url + $"query?format=json&lon={longitude}&lat={latitude}&limit=100");
        //Console.WriteLine("Kerkesa juj: " + Url + $"query?format=json&lon={longitude}&lat={latitude}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");

        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
    }

    public override JObject GetDataByTimeRange(string startTime, string endTime)
    {
        bool isDateValid = CompareDates(startTime, endTime);

        if (isDateValid)
        {
            var req = new RestRequest(Url + $"query?format=json&start={startTime}&end={endTime}&limit=20000");
            Console.WriteLine($"url: {Url + $"query?format=json&start={startTime}&end={endTime}&limit=20000"}");
            var response = HttpClient.ExecuteAsync(req);

            Console.WriteLine($"req response: {response.Result.Content}");

            return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
        }

        return new JObject(
            new JProperty("status_code", "You input invalid dates")
        );
    }

    //returns data by min magnitude
    public override dynamic GetDataByOtherQualifiers(string inputData)
    {
        var req = new RestRequest(Url + $"query?format=json&minmag={inputData}&limit=20000");
        Console.WriteLine($"url: {Url + $"query?format=json&minmag={inputData}&limit=20000"}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine(response.Result.Content);

        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
    }

    public override void XmlConversion(dynamic data)
    {
        DataConverter.Convert(data);
    }

    public override void YmlConversion(dynamic data)
    {
        DataConverter.Convert(data);
    }
}
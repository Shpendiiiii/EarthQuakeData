using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static EarthQuakeData.Utils;

namespace EarthQuakeData;

//Refined abstraction of the class DataProvider
//Makes calls to seismicportal.eu
public sealed class SpeuApi : DataProvider
{
    //The url that is read from the appsettings.json and its value set by the constructor
    public override string Url { get; init; }

    public SpeuApi(IDataConverter dataConverter, RestClient client)
    {
        Url = Wrapper.ConfigConfiguration()["url_base_paths:speu:base"]!;
        //set HttpClient to an instance of RestClient
        HttpClient = client;
        //set DataConverter prop to an instance of IDataConverter implementor
        DataConverter = dataConverter;
    }
    //Takes in todays and yesterdays data genereated by a utility method, and uses those values to make a request
    //to the api. Returns a JSON object
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
    
    //This method defines those two parameters with which it will make the request to the 
    //api to get data for a specified location
    public override JObject GetDataByLocation(string longitude, string latitude)
    {
        var req = new RestRequest(Url + $"query?format=json&lon={longitude}&lat={latitude}&limit=100");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");

        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
    }
    
    //This method returns data from a time range supplied as arguments in 'yyyy-MM-dd' format
    //First, it checks whether those dates are spaced out correctly, and then makes the request
    //If the dates are spaced out incorrectly, then it returns a JSON object telling the user as such
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

    //returns data by minimum magnitude
    public override dynamic GetDataByOtherQualifiers(string inputData)
    {
        var req = new RestRequest(Url + $"query?format=json&minmag={inputData}&limit=20000");
        Console.WriteLine($"url: {Url + $"query?format=json&minmag={inputData}&limit=20000"}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine(response.Result.Content);

        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
    }
    
    //Method that uses the concrete implementor object to convert data based on the object passed
    public override void FormatConversion(dynamic data)
    {
        DataConverter.Convert(data);
    }
    
}
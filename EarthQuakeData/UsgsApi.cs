using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static EarthQuakeData.Utils;

namespace EarthQuakeData;

//Refined abstraction that derives from DataProviders abstraction
//which makes call to the USGS Earthquake Hazards Program api
public sealed class UsgsApi : DataProvider
{
    //The url that is read from the appsettings.json and its value set by the constructor
    public override string Url { get; init; }

    public UsgsApi(IDataConverter dataConverter, RestClient client)
    {
        Url = Wrapper.ConfigConfiguration()["url_base_paths:usgs:base"]!;
        //set HttpClient to an instance of RestClient
        HttpClient = client;
        //set DataConverter prop to an instance of IDataConverter implementor passed in the constructor
        DataConverter = dataConverter;
    }

    //Takes in todays and yesterdays data genereated by a utility method, and uses those values to make a request
    //to the api. Returns a JSON object
    public override JObject GetMostRecentData()
    {
        string today = GenerateTodayYesterdayDate().Item1;
        string yesterday = GenerateTodayYesterdayDate().Item2;


        var req = new RestRequest(Url + $"query?format=geojson&starttime={yesterday}&endtime={today}");
        Console.WriteLine($"url: {Url + $"query/?format=geojson&starttime={yesterday}&endtime={today}"}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");

        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
    }
    
    //This method defines those two parameters with which it will make the request to the 
    //api to get data for a specified location
    public override JObject GetDataByLocation(string longitude, string latitude)
    {
        var req = new RestRequest(Url + $"query?format=geojson&minlongitude={longitude}&minlatitude={latitude}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");

        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
    }
    
    //This method returns data from a time range supplied as arguments in 'yyyy-MM-dd' format
    //First, it checks whether those dates are spaced out correctly, and then makes the request
    //If the dates are spaced out incorrectly, then it returns 0, then it checks that the time range
    //does not return more than the limit amount of recrods, 20000, if not then the request is made
    //and the records are returned
    public override dynamic GetDataByTimeRange(string startTime, string endTime)
    {
        bool validDates = CompareDates(startTime, endTime);

        if (validDates)
        {
            var req = new RestRequest(Url + $"count?format=geojson&starttime={startTime}&endtime={endTime}");
            Console.WriteLine($"url: {Url + $"count/?format=geojson&starttime={startTime}&endtime={endTime}"}");
            var response = HttpClient.ExecuteAsync(req);

            Console.WriteLine($"req response: {response}");
            // string count = response.Result.Content["count"];
            if (response.Result.Content != null)
            {
                var responseDes = JsonConvert.DeserializeObject<dynamic>(response.Result.Content);

                Console.WriteLine(responseDes?["count"]);
                if (responseDes?["count"] <= 20000)
                {
                    Console.WriteLine("You are allowed to do that by USGS");

                    req = new RestRequest(Url + $"query?format=geojson&starttime={startTime}&endtime={endTime}");
                    response = HttpClient.ExecuteAsync(req);
                    Console.WriteLine($"req response: {response.Result.Content}");

                    return JsonConvert.DeserializeObject<dynamic>(response.Result.Content!)!;
                }
                else
                {
                    Console.WriteLine(
                        "The USGS API does not allow for queries that result in more than 20,000 results. Shorten the time range.");

                    return 0;
                }
            }
        }

        return 0;
    }

    //this method makes requests to the api to return data based on the severity of the 
    //earthquake, the alertLevel can be red, yellow, or green
    public override JObject GetDataByOtherQualifiers(string alertLevel = "red")
    {
        var req = new RestRequest(Url + $"query?format=geojson&alertlevel={alertLevel}");
        Console.WriteLine($"url: Url + query?format=geojson&alertlevel={alertLevel}");

        var response = HttpClient.ExecuteAsync(req);
        var responseDes = JsonConvert.DeserializeObject<dynamic>(response.Result.Content!);
        // Console.WriteLine($"req response: {response.Result.Content}");
        // Console.WriteLine(responseDes);
        return responseDes!;
    }
    
    //Method that uses the concrete implementor object to convert data based on the object passed
    public override void FormatConversion(dynamic data)
    {
        DataConverter.Convert(data);
    }
}
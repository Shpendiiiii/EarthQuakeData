using Newtonsoft.Json;
using RestSharp;

namespace EarthQuakeData;

public sealed class UsgsApi : DataProvider
{
    public override string Url { get; init; }

    public UsgsApi()
    {
        // Cannot be empty
        Url = Wrapper.ConfigConfiguration()["url_base_paths:usgs:base"] ?? string.Empty;
        HttpClient = InitHttpClient();
    }


    public override void GetMostRecentData()
    {
        string today = Utils.GenerateTodayYesterdayDate().Item1;
        string yesterday = Utils.GenerateTodayYesterdayDate().Item2;


        var req = new RestRequest(Url + $"query?format=geojson&starttime={yesterday}&endtime={today}");
        Console.WriteLine($"url: {Url + $"query/?format=geojson&starttime={yesterday}&endtime={today}"}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");
    }

    public override void GetDataByLocation(string longitude, string latitude)
    {
        var req = new RestRequest(Url + $"query?format=geojson&minlongitude={longitude}&minlatitude={latitude}");
        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");
    }

    public override void GetDataByTimeRange(string startTime, string endTime)
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
                }
                else
                {
                    Console.WriteLine(
                        "The USGS API does not allow for queries that result in more than 20,000 results. Shorten the time range.");
                }
            }
        }
    }


    public override void GetDataByOtherQualifiers(string alertLevel = "red")
    {
        var req = new RestRequest(Url + $"query?format=geojson&alertlevel={alertLevel}");
        Console.WriteLine($"url: Url + query?format=geojson&alertlevel={alertLevel}");

        var response = HttpClient.ExecuteAsync(req);

        Console.WriteLine($"req response: {response.Result.Content}");
    }


    private bool CompareDates(string s, string endTime1)
    {
        DateTime start = DateTime.ParseExact(s, "yyyy-MM-dd", null);
        DateTime end = DateTime.ParseExact(endTime1, "yyyy-MM-dd", null);
        int res = DateTime.Compare(start, end);
        if (res < 0)
        {
            return true;
        }
        else if (res > 0)
        {
            Console.WriteLine("Start time should be earlier than end time");
            return false;
        }

        Console.WriteLine("Start time should be earlier than end time");
        return false;
    }
}
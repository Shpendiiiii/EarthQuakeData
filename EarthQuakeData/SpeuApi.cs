// using RestSharp;
// using static EarthQuakeData.Utils;
//
// namespace EarthQuakeData;
//
// public sealed class SpeuApi : DataProvider
// {
//     public override string Url { get; init; }
//
//     public SpeuApi()
//     {
//         Url = Wrapper.ConfigConfiguration()["url_base_paths:speu:base"] ?? string.Empty;
//         HttpClient = InitHttpClient();
//     }
//     
//     public override void GetMostRecentData()
//     {
//         string today = GenerateTodayYesterdayDate().Item1;
//         string yesterday = GenerateTodayYesterdayDate().Item2;
//         
//         var req = new RestRequest(Url + $"query?format=json&start={yesterday}&end={today}");
//         Console.WriteLine($"url: {Url + $"query?format=json&start={yesterday}&end={today}"}");
//         var response = HttpClient.ExecuteAsync(req);
//
//         Console.WriteLine($"req response: {response.Result.Content}");
//     }
//
//     public override void GetDataByLocation(string longitude, string latitude)
//     {
//         var req = new RestRequest(Url + $"query?format=json&lon={longitude}&lat={latitude}&limit=100");
//         //Console.WriteLine("Kerkesa juj: " + Url + $"query?format=json&lon={longitude}&lat={latitude}");
//         var response = HttpClient.ExecuteAsync(req);
//
//         Console.WriteLine($"req response: {response.Result.Content}");
//     }
//
//     public override void GetDataByTimeRange(string startTime, string endTime)
//     {
//         bool isDateValid = CompareDates(startTime, endTime);
//
//         if (isDateValid)
//         {
//             var req = new RestRequest(Url + $"query?format=json&start={startTime}&end={endTime}&limit=20000");
//             Console.WriteLine($"url: {Url + $"query?format=json&start={startTime}&end={endTime}&limit=20000"}");
//             var response = HttpClient.ExecuteAsync(req);
//             
//             Console.WriteLine($"req response: {response.Result.Content}");
//         }
//     }
//     
//     //returns data by min magnitude
//     public override dynamic GetDataByOtherQualifiers(string inputData)
//     {
//         var req = new RestRequest(Url + $"query?format=json&minmag={inputData}&limit=20000");
//         Console.WriteLine($"url: {Url + $"query?format=json&minmag={inputData}&limit=20000"}");
//         var response = HttpClient.ExecuteAsync(req);
//
//         Console.WriteLine(response.Result.Content);
//
//         return response.Result.Content;
//     }
// }
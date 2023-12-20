using RestSharp;

namespace EarthQuakeData;

public class Command
{
    protected RestClient httpClient { get; set; }
    protected dynamic dataConverter { get; set; }
}
using EarthQuakeData;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EQDTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        RestClient httpClient = new RestClient();

        DataProvider firstUsgs = new UsgsApi(new YmlDataConverter(), httpClient);
        
        Assert.That(firstUsgs.Url, Is.EqualTo("https://earthquake.usgs.gov/fdsnws/event/1/"));
    }
    
    [Test]
    public void Test2()
    {
        RestClient httpClient = new RestClient();

        DataProvider firstUsgs = new UsgsApi(new YmlDataConverter(), httpClient);

        JObject data = firstUsgs.GetMostRecentData();
        
        Assert.That(data, Is.TypeOf(typeof(JObject)));
    }
    
}
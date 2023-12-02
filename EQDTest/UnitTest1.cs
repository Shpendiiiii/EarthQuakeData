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
    public void UsgsApiBaseUrlShouldBeCorrect()
    {
        RestClient httpClient = new RestClient();

        DataProvider firstUsgs = new UsgsApi(new YmlDataConverter(), httpClient);
        
        Assert.That(firstUsgs.Url, Is.EqualTo("https://earthquake.usgs.gov/fdsnws/event/1/"));
    }
    
    [Test]
    public void UsgsApi_GetMostRecentData_ReturnsJObject()
    {
        RestClient httpClient = new RestClient();

        DataProvider firstUsgs = new UsgsApi(new YmlDataConverter(), httpClient);

        JObject data = firstUsgs.GetMostRecentData();
        
        Assert.That(data, Is.TypeOf(typeof(JObject)));
    }

    [Test]
    public void SpeuApi_GetMostRecentData_ReturnsJObject()
    {
        RestClient httpClient = new RestClient();

        DataProvider firtSpeu = new SpeuApi(new XmlDataConverter(), httpClient);

        JObject data = firtSpeu.GetMostRecentData();
        
        Assert.That(data, Is.TypeOf(typeof(JObject)));
    }

    [Test]
    public void SepuApiBaseUrlShouldBeCorrect()
    {
        RestClient httpClient = new RestClient();
        
        DataProvider firstUsgs = new SpeuApi(new YmlDataConverter(), httpClient);

        Assert.That(firstUsgs.Url, Is.EqualTo("https://www.seismicportal.eu/fdsnws/event/1/"));
    }

    [Test]
    public void GetDataByOtherQualifiers_ReturnsJObjectForQualifier5()
    {
        RestClient httpClient = new RestClient();
        
        DataProvider firstSpeu = new SpeuApi(new YmlDataConverter(), httpClient);

        JObject data = firstSpeu.GetDataByOtherQualifiers("5");
        
        Assert.That(data, Is.TypeOf(typeof(JObject)));
    }
   //TODO meaningful names for tests 
}
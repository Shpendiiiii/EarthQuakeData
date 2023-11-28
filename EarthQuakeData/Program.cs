using System.Threading.Channels;
using EarthQuakeData;



// Console.WriteLine(Wrapper.ConfigConfiguration()["url_base_paths:usgs:base"]);



DataProvider firstUsgs = new UsgsApi();

// firstUsgs.GetMostRecentData();

dynamic info = firstUsgs.GetDataByOtherQualifiers("yellow");

string xml = firstUsgs.XmlConversion(info);

Console.WriteLine(xml);

// firstUsgs.GetDataByTimeRange(startTime: "2023-11-04", endTime: "2023-11-05");

// firstUsgs.GetDataByLocation(longitude: "39.82", latitude: "21.74");


// DataProvider firstSPEU = new SpeuApi();

// firstSPEU.GetMostRecentData();

// firstSPEU.GetDataByLocation(longitude: "42.66296", latitude: "21.16645");

// firstSPEU.GetDataByTimeRange("2023-11-26", "2023-11-28");

// firstSPEU.GetDataByOtherQualifiers("6");
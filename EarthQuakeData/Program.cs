﻿using EarthQuakeData;
using Newtonsoft.Json.Linq;
using RestSharp;


RestClient client = new RestClient();

DataProvider firstUsgs = new UsgsApi(new XmlDataConverter(), client);


dynamic info = firstUsgs.GetDataByOtherQualifiers("green");
firstUsgs.FormatConversion(info);
firstUsgs = new UsgsApi(new YmlDataConverter(), client);
firstUsgs.FormatConversion(info);





// firstUsgs.GetDataByTimeRange(startTime: "2023-11-04", endTime: "2023-11-05");

// firstUsgs.GetDataByLocation(longitude: "39.82", latitude: "21.74");


// DataProvider firstSPEU = new SpeuApi();

// firstSPEU.GetMostRecentData();

// firstSPEU.GetDataByLocation(longitude: "42.66296", latitude: "21.16645");

// firstSPEU.GetDataByTimeRange("2023-11-26", "2023-11-28");

// firstSPEU.GetDataByOtherQualifiers("6");


Console.WriteLine(Directory.GetCurrentDirectory());
Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
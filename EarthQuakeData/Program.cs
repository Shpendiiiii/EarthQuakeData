using EarthQuakeData;



// Console.WriteLine(Wrapper.ConfigConfiguration()["url_base_paths:usgs:base"]);



DataProvider firstUsgs = new UsgsApi();

// firstUsgs.GetMostRecentData();

// firstUsgs.GetDataByOtherQualifiers("yellow");

// firstUsgs.GetDataByTimeRange(startTime: "2023-11-04", endTime: "2023-11-05");

firstUsgs.GetDataByLocation(longitude: "21.165249677022974", latitude: "47.66372");
using System.Runtime.CompilerServices;

namespace EarthQuakeData;

public static class Utils
{
    public static Tuple<string, string> GenerateTodayYesterdayDate()
    {
        // Format the dates as strings in the "yyyy-MM-dd" format
        Tuple<string, string> recentDates = Tuple.Create(
            //today
            DateTime.Today.ToString("yyyy-MM-dd"),
            //yesterday
            DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd")
        );

        return recentDates;
    }
}
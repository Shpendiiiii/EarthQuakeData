//Utility classes for specific needs
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

    public static bool CompareDates(string s, string endTime1)
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
using System;

namespace OctopusFramework.V2.Common
{
    public static class DateTimeHelper
    {
        public static DateTime FirstDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDay(this DateTime dt)
        {
            return dt.FirstDay().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        public static DateTime LastTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }

        public static DateTime PreviousMonth(this DateTime dt)
        {
            return dt.FirstDay().AddMonths(-1);
        }

        public static DateTime NextMonth(this DateTime dt)
        {
            return dt.FirstDay().AddMonths(1);
        }

        public static DateTime PreviousYear(this DateTime dt)
        {
            return dt.FirstDay().AddYears(-1);
        }

        public static DateTime NextYear(this DateTime dt)
        {
            return dt.FirstDay().AddYears(1);
        }

        public static TimeSpan BetweenDate(this DateTime st, DateTime ed)
        {
            TimeSpan result = (ed > st) ? ed - st : st - ed;
            return result;
        }

    }
}

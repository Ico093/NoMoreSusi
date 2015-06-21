using DDay.iCal;

namespace NoMoreSusi.Common.Helpers
{
    public static class CalendarFactory
    {
        public static iCalendar GetCalendar(string method, string version)
        {
            var calendar = new iCalendar
            {
                Method = method,
                Version = version
            };

            return calendar;
        }
    }
}

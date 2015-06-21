

using System;
using System.Text;
using System.Web.Mvc;
using DDay.iCal;
using DDay.iCal.Serialization;
using DDay.iCal.Serialization.iCalendar;

namespace NoMoreSusi.Common.Helpers
{
    public static class CalendarHelper
    {
        public static void CreateCalendarEvent(iCalendar calendar, string title, string description, int hour, DayOfWeek dayOfWeek, int duration,
            string location, string organizer, bool allDayEvent)
        {
            DateTime today = DateTime.Today;
            int daysUntilNextDay = ((int)dayOfWeek - (int)today.DayOfWeek + 7) % 7;
            DateTime nextDay = today.AddDays(daysUntilNextDay);
            DateTime startDate = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, hour, 0, 0);

            Event evt = calendar.Create<Event>();
            evt.Start = (iCalDateTime)startDate;
            evt.End = evt.Start.AddHours(duration);
            evt.Description = description;
            evt.Location = location;
            evt.Summary = title;
            evt.IsAllDay = allDayEvent;
            evt.Organizer = new Organizer(organizer);

            IRecurrencePattern pattern = new RecurrencePattern();
            pattern.Frequency = FrequencyType.Weekly;
            pattern.ByDay.Add(new WeekDay(dayOfWeek));
            evt.RecurrenceRules.Add(pattern);
        }

        public static byte[] ExportToByteArray(iCalendar calendar)
        {
            ISerializationContext ctx = new SerializationContext();
            ISerializerFactory factory = new SerializerFactory();
            IStringSerializer serializer = factory.Build(calendar.GetType(), ctx) as IStringSerializer;

            string output = serializer.SerializeToString(calendar);
            var bytes = Encoding.UTF8.GetBytes(output);
            return bytes;
        }
    }
}

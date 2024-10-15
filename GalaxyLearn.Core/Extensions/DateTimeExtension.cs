using System.Globalization;

namespace GalaxyLearn.Core.Extensions
{
    public static class DateTimeExtention
    {
        public static string ToPersianDate(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int dayOfMonth = persianCalendar.GetDayOfMonth(date);

            return (year.ToString() + "/" + month.ToString("00") + "/" + dayOfMonth.ToString("00"));
        }

        public static string ToPersianDateTime(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int dayOfMonth = persianCalendar.GetDayOfMonth(date);
            int hour = persianCalendar.GetHour(date);
            int minute = persianCalendar.GetMinute(date);

            return
                hour.ToString() + ":" +
                minute.ToString() + " " +
                year.ToString() + "/" +
                month.ToString("00") + "/" +
                dayOfMonth.ToString("00");
        }

        public static string GetTime(this DateTime date)
        {
            return date.ToString("HH:mm:ss");
        }
    }
}


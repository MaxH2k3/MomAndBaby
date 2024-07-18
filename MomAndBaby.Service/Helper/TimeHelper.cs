using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Helper
{
    public class TimeHelper
    {
        public static string GetTimeSender(DateTime createdDate)
        {
            var currentTime = GetCurrentInVietName();

            var timeSpan = currentTime - createdDate;

            if(timeSpan.TotalDays > 90)
            {
                return createdDate.ToString("dd/MM/yyyy");
            }

            if (timeSpan.TotalDays > 1)
            {
                return ((int)(timeSpan.TotalDays)).ToString() + "d ago";
            }

            if (timeSpan.TotalHours > 1)
            {
                return ((int)(timeSpan.TotalHours)).ToString() + "h ago";
            }

            if (timeSpan.TotalMinutes > 1)
            {
                return ((int)(timeSpan.TotalMinutes)).ToString() + "m ago";
            }

            return ((int)(timeSpan.TotalSeconds)).ToString() + "s ago";
        }


        public static DateTime GetCurrentInVietName()
        {
            // Specify the time zone ID for Vietnam
            const string timeZoneId = "SE Asia Standard Time"; // This is the ID for the time zone of Vietnam

            // Get the time zone information for Vietnam
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Get the current date and time in Vietnam
            var nowInVietnam = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

            // Display the current date and time in Vietnam
            return nowInVietnam;
        }

    }
}

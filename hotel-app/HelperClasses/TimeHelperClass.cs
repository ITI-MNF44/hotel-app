using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClass
{
    public static class TimeHelperClass
    {
        public static DateTime getCurrTime()
        {
            return DateTime.Now;
        }
        public static string getCustomTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd hh:mm:ss tt");
        }

    }
}

using Com.DaacoWorks.Protocol.Logger;
using System;
using System.IO;
using System.Text;

namespace Com.DaacoWorks.Protocol.Util
{
    /// <summary>
    /// ProtocolUtils class to provide utility methods.
    /// </summary>
    public class ProtocolUtils
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ProtocolUtils).FullName);
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Gets totl milliseconds since 1st Jan 1970
        /// </summary>
        /// <returns></returns>
        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        /// <summary>
        /// Gets datetime from totl milliseconds since 1st Jan 1970
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTimeFromCurrentTimeMillis(long currentTimeMillis)
        {
            var milliseconds = currentTimeMillis % 1000;
            var seconds = currentTimeMillis / 1000;           
            var minutes = seconds / 60;
            seconds = seconds % 60;
            var hours = minutes / 60;
            minutes = minutes % 60;
            var days = hours / 24;
            hours = hours % 24;
            var dateTime = Jan1st1970.AddDays(days);
            dateTime = dateTime.AddHours(hours);
            dateTime = dateTime.AddMinutes(minutes);
            dateTime = dateTime.AddSeconds(seconds);
            dateTime = dateTime.AddMilliseconds(milliseconds);
            return dateTime.ToLocalTime();
        }

        /// <summary>
        /// Gets the hex byte string.
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <returns>the hex byte string</returns>
        public static String GetHexByteString(MemoryStream buffer)
        {
            StringBuilder strBuffer = new StringBuilder();
            int data = -1;
            while ((data=buffer.ReadByte()) >= 0)
            {
                strBuffer.Append(string.Format("{0:X2}", data & 0x000000FF));
            }
            return strBuffer.ToString();
        }

    }
}
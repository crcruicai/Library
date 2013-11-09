/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/31 11:06:29
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Util
{
    /// <summary>
    /// DateTime 工具.
    /// </summary>
    public class DateTimeUtil
    {

        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <returns></returns>
        public static Int64 GetValidityNum()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// 以UTC时间为准的时间戳
        }
        /// <summary>
        /// 获取指定时间的时间戳.
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static Int64 GetValidityNum(DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// 以UTC时间为准的时间戳
        }

        /// <summary>
        /// 将nuix中的日期格式转换成正常日期格式，前提传入的格式正确
        /// </summary>
        /// <param name="timestampString">传入的时间戳</param>
        /// <returns></returns>
        public static String ConvertToWin(String timestampString)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestampString + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 将nuix中的日期格式转换成日期时间
        /// </summary>
        /// <param name="timestam">传入的时间戳</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.Add(new TimeSpan(timestamp * 10000000));
        }
        /// <summary>
        /// 将nuix中的日期格式转换成日期时间
        /// </summary>
        /// <param name="timestampString">传入的时间戳</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(String timestampString)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestampString + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 将当前日期时间转换成unix日期时间戳格式
        /// </summary>
        /// <returns>unix时间</returns>
        public static string ConvertToUnix()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = DateTime.Now.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }
        /// <summary>
        /// 将当前日期时间转换成unix日期时间戳格式
        /// </summary>
        /// <returns>unix时间</returns>
        public static long ConvertToUnixofLong()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = DateTime.Now.Subtract(dtStart);
            return toNow.Ticks / 10000000;
        }
        /// <summary>
        /// 将正常的日期转换成unix日期时间戳格式
        /// </summary>
        /// <param name="datetime">正常日期转换成的字符串格式如：yyyy-MM-dd HH:mm:ss</param>
        /// <returns>unix时间</returns>
        public static string ConvertToUnix(DateTime datetime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = datetime.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }

        /// <summary>
        /// 获取指定日期的星期,并转换为中文格式.
        /// <para>如:2013-10-31 为星期四,返回为周四或星期四.</para>
        /// </summary>
        /// <param name="now">指定日期</param>
        /// <param name="type">true,为星期* flase为周*</param>
        /// <returns></returns>
        public static String GetDayOfWeek(DateTime now,bool type)
        {
            switch (Convert.ToInt32(now.DayOfWeek))
            {
                case 0: return type? "星期天":"周日";
                case 1: return type ? "星期一" : "周一";
                case 2: return type ? "星期二" : "周二";
                case 3: return type ? "星期三" : "周三";
                case 4: return type ? "星期四" : "周四";
                case 5: return type ? "星期五" : "周五";
                case 6: return type ? "星期六" : "周六";
            }
            return string.Empty;
        }

    }
}

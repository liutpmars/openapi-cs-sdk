﻿using System;
namespace TigerOpenAPI.Common.Util
{
  public class DateUtil
  {
    public const string FORMAT_DATETIME_MS_WITH_UTC_OFFSET = "yyyy-MM-dd HH:mm:ss.fff zzz";
    public const string FORMAT_DATETIME_MS = "yyyy-MM-dd HH:mm:ss.fff";
    public const string FORMAT_DATETIME = "yyyy-MM-dd HH:mm:ss";
    public const string FORMAT_DATE = "yyyy-MM-dd";
    public const string DEFAULT_TIME_SUFFIX = " 00:00:00";

    public static readonly TimeZoneInfo HK_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Hong_Kong");
    public static readonly TimeZoneInfo SH_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai");
    public static readonly TimeZoneInfo NY_ZONE = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

    private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    private DateUtil()
    {
    }

    public static long CurrentTimeMillis()
    {
      return (long)(DateTimeOffset.UtcNow.DateTime - Jan1st1970).TotalMilliseconds;
    }

    public static DateTime? ConvertTime(string datetime, TimeZoneInfo timeZoneInfo)
    {
      if (DateTime.TryParse(datetime, out DateTime curDateTime)) {
        return TimeZoneInfo.ConvertTime(curDateTime, timeZoneInfo);
      }
      return null;
    }

    public static long ConvertTimestamp(string datetime, TimeZoneInfo timeZoneInfo)
    {
      if (DateTime.TryParse(datetime, out DateTime curDateTime))
      {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(curDateTime, timeZoneInfo.GetUtcOffset(curDateTime));
        return (long)(dateTimeOffset - Jan1st1970).TotalMilliseconds;
      }
      return 0;
    }

    /**
     * print date(yyyy-MM-dd)
     * @param timestamp milliseconds
     * @return
     */
    public static string PrintDate(long timestamp, TimeZoneInfo timeZoneInfo)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime,
        timeZoneInfo).ToString(FORMAT_DATE);
    }

    /**
     * print datetime(yyyy-MM-dd HH:mm:ss)
     * @param timestamp milliseconds
     * @return
     */
    public static string PrintDateTime(long timestamp, TimeZoneInfo timeZoneInfo)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime,
        timeZoneInfo).ToString(FORMAT_DATETIME);
    }

    /**
     * print datetime(yyyy-MM-dd HH:mm:ss.fff)
     * @param timestamp milliseconds
     * @return
     */
    public static string PrintDateTimeWithMs(long timestamp, TimeZoneInfo timeZoneInfo)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime,
        timeZoneInfo).ToString(FORMAT_DATETIME_MS);
    }

    /**
     * get utc system date(yyyy-MM-dd)
     * @return
     */
    public static string PrintUtcSystemDate()
    {
      return DateTimeOffset.UtcNow.ToString(FORMAT_DATE);
    }

    /**
     * get system date(yyyy-MM-dd)
     * @return
     */
    public static string PrintSystemDate(TimeZoneInfo timeZoneInfo)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.UtcNow.DateTime, timeZoneInfo).ToString(FORMAT_DATE);
    }

    /**
     * get utc system datetime(yyyy-MM-dd HH:mm:ss)
     * @return
     */
    public static string PrintUtcSystemDateTime()
    {
      return DateTimeOffset.UtcNow.ToString(FORMAT_DATETIME);
    }

    /**
     * get system datetime(yyyy-MM-dd HH:mm:ss)
     * @return
     */
    public static string PrintSystemDateTime(TimeZoneInfo timeZoneInfo)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.UtcNow.DateTime, timeZoneInfo).ToString(FORMAT_DATETIME);
    }
  }
}


using System;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// DateTime扩展方法
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取一个时间戳 - 秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToUnixSeconds(this DateTime time) => new DateTimeOffset(time).ToUniversalTime().ToUnixTimeSeconds();
        /// <summary>
        /// 获取一个时间戳 - 毫秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToUnixMilliSeconds(this DateTime time) => new DateTimeOffset(time).ToUniversalTime().ToUnixTimeMilliseconds();
    }
}

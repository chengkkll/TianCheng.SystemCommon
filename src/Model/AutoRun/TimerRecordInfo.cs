using System;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 定时运行记录
    /// </summary>
    public class TimerRecordInfo : BusinessMongoModel
    {
        /// <summary>
        /// 定时器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 运行时间
        /// </summary>
        public DateTime RunTime { get; set; }
        /// <summary>
        /// 运行结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 耗时 秒
        /// </summary>
        public double TotalSeconds { get; set; }

    }
}

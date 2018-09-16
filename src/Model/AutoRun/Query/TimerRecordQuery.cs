using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 按定时器查询
    /// </summary>
    public class TimerRecordQuery : QueryInfo
    {
        /// <summary>
        /// 按定时器名称模糊查询
        /// </summary>
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 定时运行记录
    /// </summary>
    [DBMapping("system_timer_record")]
    public class TimerRecordDAL : MongoOperation<TimerRecordInfo>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 行业信息 数据持久化操作
    /// </summary>
    [DBMapping("system_industry")]
    public class IndustryDAL : MongoOperation<IndustryInfo>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 部门信息 [数据持久化]
    /// </summary>
    [DBMapping("System_DepartmentInfo")]
    public class DepartmentDAL : MongoOperation<DepartmentInfo>
    {
    }
}

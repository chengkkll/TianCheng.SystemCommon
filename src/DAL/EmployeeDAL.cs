﻿using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 员工信息 [数据持久化]
    /// </summary>
    [DBMapping("System_EmployeeInfo")]
    public class EmployeeDAL : MongoOperation<EmployeeInfo>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianCheng.BaseService.Services;
using TianCheng.DAL.Redis;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon
{
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentCache : RedisHashOperation<DepartmentInfo>, BaseService.IServiceRegister
    {
        /// <summary>
        /// 
        /// </summary>
        public DepartmentCache()
            : base("department")
        {

        }
    }
}

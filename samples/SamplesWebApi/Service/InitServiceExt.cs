using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianCheng.BaseService;

namespace SamplesWebApi.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class InitServiceExt : IServiceExtOption
    {
        /// <summary>
        /// 
        /// </summary>
        public void SetOption()
        {
            TianCheng.SystemCommon.Services.DepartmentService.OnSaveCheck += DepartmentServiceTestExt.Test;
            TianCheng.SystemCommon.Services.DepartmentService.OnSaveCheck += DepartmentServiceTestExt.Test2;

            TianCheng.SystemCommon.Services.AuthService.OnLogin += OnlineEmployeeService.OnLogin;
            TianCheng.SystemCommon.Services.AuthService.OnLogout += OnlineEmployeeService.OnLogout;
        }
    }
}

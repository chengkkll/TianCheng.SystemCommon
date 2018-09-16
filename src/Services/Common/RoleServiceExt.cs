using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.BaseService.Services;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 角色的事件处理
    /// </summary>
    public class RoleServiceExt : IServiceExtOption
    {
        /// <summary>
        /// 设置扩展处理
        /// </summary>
        public void SetOption()
        {
            // 更新的后置处理
            RoleService.OnUpdated += EmployeeService.OnRoleUpdated;
        }
    }
}

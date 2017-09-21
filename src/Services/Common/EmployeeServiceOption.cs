using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 员工管理服务的配置信息
    /// </summary>
    public class EmployeeServiceOption
    {
        #region 单件处理
        /// <summary>
        /// 
        /// </summary>
        static private EmployeeServiceOption _Option = new EmployeeServiceOption();
        /// <summary>
        /// 
        /// </summary>
        static public EmployeeServiceOption Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        private EmployeeServiceOption()
        {

        }
        #endregion

        #region 扩展的事件处理
        /// <summary>
        /// 保存的校验的扩展处理
        /// </summary>
        public Action<EmployeeInfo, TokenLogonInfo> SavingCheck;
        /// <summary>
        /// 保存的前置处理
        /// </summary>
        public Action<EmployeeInfo, TokenLogonInfo> Saving;
        /// <summary>
        /// 保存的后置处理
        /// </summary>
        public Action<EmployeeInfo, TokenLogonInfo> Saved;
        /// <summary>
        /// 更新的后置处理
        /// </summary>
        public Action<EmployeeInfo, EmployeeInfo, TokenLogonInfo> Updated;
        #endregion

        #region 数据验证的配置
        private bool _RequiredRole = true;
        /// <summary>
        /// 设置角色是否必填
        /// </summary>
        public bool RequiredRole { get { return _RequiredRole; } set { _RequiredRole = value; } }

        private bool _RequiredDepartment = true;
        /// <summary>
        /// 设置部门是否必填
        /// </summary>
        public bool RequiredDepartment { get { return _RequiredDepartment; } set { _RequiredDepartment = value; } }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 部门管理服务的配置信息
    /// </summary>
    public class DepartmentServiceOption
    {
        #region 单件处理
        /// <summary>
        /// 
        /// </summary>
        static private DepartmentServiceOption _Option = new DepartmentServiceOption();
        /// <summary>
        /// 
        /// </summary>
        static public DepartmentServiceOption Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        private DepartmentServiceOption()
        {

        }
        #endregion

        #region 扩展的事件处理
        /// <summary>
        /// 保存的校验的扩展处理
        /// </summary>
        public Action<DepartmentInfo, TokenLogonInfo> SavingCheck;
        /// <summary>
        /// 更新部门的后置处理，第一个参数为新部门信息，第二个参数为原部门信息，第三个参数为当前操作人的登录信息
        /// </summary>
        public Action<DepartmentInfo,DepartmentInfo,TokenLogonInfo> Updated;
        #endregion

    }
}

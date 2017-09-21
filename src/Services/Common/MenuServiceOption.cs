using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 菜单管理服务的配置信息
    /// </summary>
    public class MenuServiceOption
    {
        #region 单件处理
        /// <summary>
        /// 
        /// </summary>
        static private MenuServiceOption _Option = new MenuServiceOption();
        /// <summary>
        /// 
        /// </summary>
        static public MenuServiceOption Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        private MenuServiceOption()
        {

        }
        #endregion

        #region 扩展的事件处理
        /// <summary>
        /// 完善菜单数据的扩展处理
        /// </summary>
        public Action<List<MenuMainInfo>> InitMenuData;
        #endregion
    }
}

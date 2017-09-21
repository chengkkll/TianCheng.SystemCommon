using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TianCheng.Model;
using TianCheng.DAL.MongoDB;

namespace TianCheng.SystemCommon.Model
{    
    /// <summary>
    /// 菜单信息
    /// </summary>
    [CollectionMapping("System_MenuInfo")]
    public class MenuMainInfo : BusinessMongoModel
    {
        /// <summary>
        /// 菜单序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 菜单的定位
        /// </summary>
        public string Sref { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }
        /// <summary>
        /// 字体图标
        /// </summary>
        public string FontAwesome { get; set; }

        private List<MenuSubInfo> _SubMenu = new List<MenuSubInfo>();
        /// <summary>
        /// 子菜单列表 
        /// </summary>
        public List<MenuSubInfo> SubMenu
        {
            get { return _SubMenu; }
            set { _SubMenu = value; }
        }
        
    }
}

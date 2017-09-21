using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 主菜单信息
    /// </summary>
    public class MenuMainView : BaseViewModel
    {
        /// <summary>
        /// 菜单序号
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 菜单的定位
        /// </summary>
        [JsonProperty("sref")]
        public string Sref { get; set; }

        /// <summary>
        /// 字体图标
        /// </summary>
        [JsonProperty("font_awesome")]
        public string FontAwesome { get; set; }

        private List<MenuSubView> _sub = new List<MenuSubView>();
        /// <summary>
        /// 子菜单列表
        /// </summary>
        [JsonProperty("sub_menu")]
        public List<MenuSubView> SubMenu
        {
            get { return _sub; }
            set { _sub = value; }
        }
    }
}

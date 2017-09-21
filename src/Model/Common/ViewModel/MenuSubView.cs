using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 子菜单信息
    /// </summary>
    public class MenuSubView
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

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
    }
}

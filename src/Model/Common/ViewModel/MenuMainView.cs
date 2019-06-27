using Newtonsoft.Json;
using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 主菜单信息
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
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
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        [JsonProperty("sub")]
        public List<MenuSubView> SubMenu { get; set; } = new List<MenuSubView>();
    }
}

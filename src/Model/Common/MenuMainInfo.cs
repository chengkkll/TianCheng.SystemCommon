using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
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
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 菜单的定位
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 子菜单列表 
        /// </summary>
        public List<MenuSubInfo> SubMenu { get; set; } = new List<MenuSubInfo>();
    }
}

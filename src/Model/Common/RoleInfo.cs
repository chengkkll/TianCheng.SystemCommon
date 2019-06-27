using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 角色信息
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class RoleInfo : BusinessMongoModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 当前角色登录后的默认页面
        /// </summary>
        public string DefaultPage { get; set; }
        /// <summary>
        /// 包含菜单列表
        /// </summary>
        public List<MenuMainView> PagePower { get; set; } = new List<MenuMainView>();
        /// <summary>
        /// 包含功能点列表
        /// </summary>
        public List<FunctionView> FunctionPower { get; set; } = new List<FunctionView>();
        /// <summary>
        /// 是否为系统级别数据
        /// </summary>
        public bool IsSystem { get; set; }
    }
}

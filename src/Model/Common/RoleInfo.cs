using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using TianCheng.Model;
using TianCheng.DAL.MongoDB;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 角色信息
    /// </summary>
    [CollectionMapping("System_RoleInfo")]
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
        /// 菜单列表
        /// </summary>
        private List<MenuMainView> _PagePower = new List<MenuMainView>();
        /// <summary>
        /// 包含菜单列表
        /// </summary>
        public List<MenuMainView> PagePower { get { return _PagePower; } set { _PagePower = value; } }


        private List<FunctionView> _FunctionPower = new List<FunctionView>();
        /// <summary>
        /// 包含功能点列表
        /// </summary>
        public List<FunctionView> FunctionPower { get { return _FunctionPower; } set { _FunctionPower = value; } }

        /// <summary>
        /// 是否为系统级别数据
        /// </summary>
        public bool IsSystem { get; set; }
    }
}

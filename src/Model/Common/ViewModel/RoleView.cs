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
    /// 角色查看信息
    /// </summary>
    public class RoleView : BaseViewModel
    {
        /// <summary>
        /// 描述信息
        /// </summary>
        [JsonProperty("desc")]
        public string Desc { get; set; }
        /// <summary>
        /// 当前角色登录后的默认页面
        /// </summary>
        [JsonProperty("page")]
        public string DefaultPage { get; set; }

        /// <summary>
        /// 包含菜单列表
        /// </summary>
        [JsonProperty("menu")]
        public List<MenuMainView> PagePower { get; set; }

        /// <summary>
        /// 包含功能点列表
        /// </summary>
        [JsonProperty("functions")]
        public List<FunctionView> FunctionPower { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [JsonProperty("update_date")]
        public string UpdateDate { get; set; }
        /// <summary>
        /// 最后更新人
        /// </summary>
        [JsonProperty("update_user")]
        public string UpdaterName { get; set; }

        /// <summary>
        /// 是否为系统级别数据
        /// </summary>
        [JsonProperty("is_system")]
        public bool IsSystem { get; set; }
    }
}

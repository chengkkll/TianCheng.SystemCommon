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
    /// 部门信息  [查看对象]
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DepartmentView : BaseViewModel
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 排序的序号
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }
        /// <summary>
        /// 部门描述
        /// </summary>
        [JsonProperty("desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        [JsonProperty("parent_name")]
        public string ParentName { get; set; }

        /// <summary>
        /// 部门主管ID
        /// </summary>
        [JsonProperty("manage_id")]
        public string ManageId { get; set; }
        /// <summary>
        /// 部门主管名称
        /// </summary>
        [JsonProperty("manage_name")]
        public string ManageName { get; set; }

        /// <summary>
        /// 包含的子部门
        /// </summary>
        [JsonProperty("sub")]
        public List<BaseViewModel> SubList { get; set; }

        /// <summary>
        /// 包含的行业列表
        /// </summary>
        [JsonProperty("industries")]
        public List<BaseViewModel> Industries { get; set; }

        /// <summary>
        /// 包含的员工列表 仅用于查询时方便（查询单条部门时会有数据），新增修改时不需要传递
        /// </summary>
        [JsonProperty("employees")]
        public List<SelectView> Employees { get; set; }

        /// <summary>
        /// 最后更新时间  仅用于列表显示及排序，新增修改时不需要传递
        /// </summary>
        [JsonProperty("update_date")]
        public string UpdateDate { get; set; }
        /// <summary>
        /// 最后更新人  仅用于列表显示及排序，新增修改时不需要传递
        /// </summary>
        [JsonProperty("update_user")]
        public string UpdaterName { get; set; }

        /// <summary>
        /// 扩展ID 用于部门信息的扩展
        /// </summary>
        [JsonProperty("ext_id")]
        public string ExtId { get; set; }
    }
}

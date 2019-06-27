using Newtonsoft.Json;
using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 部门信息  [查看对象]
    /// </summary>
    public class DepartmentView : BaseViewModel
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排序的序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 部门描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 部门主管ID
        /// </summary>
        public string ManageId { get; set; }
        /// <summary>
        /// 部门主管名称
        /// </summary>
        public string ManageName { get; set; }

        /// <summary>
        /// 包含的子部门
        /// </summary>
        [JsonProperty("sub")]
        public List<BaseViewModel> SubList { get; set; }

        /// <summary>
        /// 最后更新时间  仅用于列表显示及排序，新增修改时不需要传递
        /// </summary>
        public string UpdateDate { get; set; }
        /// <summary>
        /// 最后更新人  仅用于列表显示及排序，新增修改时不需要传递
        /// </summary>
        public string UpdaterName { get; set; }

        /// <summary>
        /// 扩展ID 用于部门信息的扩展
        /// </summary>
        public string ExtId { get; set; }
    }
}

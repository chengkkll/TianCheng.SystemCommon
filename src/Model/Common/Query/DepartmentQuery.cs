using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 部门的查询条件
    /// </summary>
    public class DepartmentQuery : TianCheng.Model.QueryInfo
    {
        /// <summary>
        /// 按名称模糊查询
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 按上级部门ID查询
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// 按部门主管ID查询
        /// </summary>
        [JsonProperty("manage_id")]
        public string ManageId { get; set; }

        /// <summary>
        /// 按部门主管的名称模糊查询
        /// </summary>
        [JsonProperty("manage_name")]
        public string ManageName { get; set; }

        /// <summary>
        /// 按所属行业id列表查询。不能传入一级行业。以数组的方式提交行业id，例如：[afd321,adfiop]
        /// </summary>
        [JsonProperty("industries")]
        public List<string> Industries { get; set; }
    }
}

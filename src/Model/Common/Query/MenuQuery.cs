using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 菜单的查询条件
    /// </summary>
    public class MenuQuery : TianCheng.Model.QueryInfo
    {
        /// <summary>
        /// 按名称模糊查询
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}

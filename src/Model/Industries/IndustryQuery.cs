using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 行业查询条件
    /// </summary>
    public class IndustryQuery : QueryInfo
    {
        /// <summary>
        /// 按编码、名称、说明模糊查询
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 根据行业ID列表
        /// </summary>
        [JsonProperty("ids")]
        public List<string> IndustryIdList { get; set; }
    }
}

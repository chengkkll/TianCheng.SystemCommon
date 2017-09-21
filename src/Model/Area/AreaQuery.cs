using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 区域信息查询条件
    /// </summary>
    public class AreaQuery : TianCheng.Model.QueryInfo
    {
        /// <summary>
        /// 按区域名称模糊查询
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 按区域编码查询
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 按上级区域ID查询
        /// </summary>
        [JsonProperty("superior_code")]
        public string SuperiorCode { get; set; }
        /// <summary>
        /// 按上级区域名称查询
        /// </summary>
        [JsonProperty("superior_name")]
        public string SuperiorName { get; set; }
        /// <summary>
        /// 按区域类型查询
        /// </summary>
        [JsonProperty("area_type")]
        public AreaType AreaType { get; set; }
    }
}

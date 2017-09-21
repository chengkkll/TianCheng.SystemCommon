using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 区域信息 的查看对象
    /// </summary>
    public class AreaView
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 区域邮编
        /// </summary>
        [JsonProperty("zip")]
        public string Zip { get; set; }
        /// <summary>
        /// 电话区号
        /// </summary>
        [JsonProperty("telephone_code")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 上级区域ID
        /// </summary>
        [JsonProperty("superior_Code")]
        public string SuperiorCode { get; set; }
        /// <summary>
        /// 上级区域名称
        /// </summary>
        [JsonProperty("superior_name")]
        public string SuperiorName { get; set; }
        /// <summary>
        /// 区域类型
        /// </summary>
        [JsonProperty("area_type")]
        public AreaType AreaType { get; set; }
    }
}

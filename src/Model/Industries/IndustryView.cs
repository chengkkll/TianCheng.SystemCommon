using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 行业信息  [查看对象]
    /// </summary>
    public class IndustryView
    {
        /// <summary>
        /// ID 新增时不需要传值，修改的时候需要传递
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 行业说明
        /// </summary>
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [JsonProperty("updateDate")]
        public string UpdateDate { get; set; }
        /// <summary>
        /// 最后更新人
        /// </summary>
        [JsonProperty("updateUser")]
        public string UpdaterName { get; set; }
        /// <summary>
        /// 子行业列表
        /// </summary>
        [JsonProperty("sub")]
        public List<IndustryView> Sub { get; set; } = new List<IndustryView>();

    }
}

using Newtonsoft.Json;
using System;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class DataImportIndexView
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 导入的批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 导入数据总数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 已导入完成数量
        /// </summary>
        public int Current { get; set; }
        /// <summary>
        /// 是否导入完成
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [JsonProperty("creater")]
        public string CreaterName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("date")]
        public DateTime CreateDate { get; set; }
    }
}

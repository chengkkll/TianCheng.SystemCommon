using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 功能点对象 查看对象
    /// </summary>
    public class FunctionView 
    {
        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }
        ///// <summary>
        ///// 权限编码
        ///// </summary>
        //[JsonProperty("code")]
        //public string Code { get; set; }
        /// <summary>
        /// 功能点标识名      可以理解为唯一标识功能点的ID值
        /// </summary>
        [JsonProperty("policy")]
        public string Policy { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}

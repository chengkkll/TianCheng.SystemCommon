using Newtonsoft.Json;
using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 功能点模块
    /// </summary>
    public class FunctionModuleView : BaseViewModel
    {
        /// <summary>
        /// 模块序号
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }
        /// <summary>
        /// 模块编码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        private List<FunctionGroupInfo> _FunctionGroups = new List<FunctionGroupInfo>();
        /// <summary>
        /// 功能点分组（Control）
        /// </summary>
        [JsonProperty("group")]
        public List<FunctionGroupInfo> FunctionGroups
        {
            get { return _FunctionGroups; }
            set { _FunctionGroups = value; }
        }
    }
}

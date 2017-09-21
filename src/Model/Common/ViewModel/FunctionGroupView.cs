using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 功能点分类 查看对象
    /// </summary>
    public class FunctionGroupView 
    {
        /// <summary>
        /// 分组序号
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [JsonProperty("name")]
        public int Name { get; set; }
        /// <summary>
        /// 分组编码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        private List<FunctionView> _functions = new List<FunctionView>();
        /// <summary>
        /// 包含功能点列表
        /// </summary>
        [JsonProperty("functions")]
        public List<FunctionView> FunctionList
        {
            get { return _functions; }
            set { _functions = value; }
        }
    }
}

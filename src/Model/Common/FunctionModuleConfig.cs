using System.Collections.Generic;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 模块的配置信息，用于读取写在appsettings.json中的信息
    /// </summary>
    public class FunctionModuleConfig
    {
        /// <summary>
        /// 获取模块的命名空间及模块名称
        /// </summary>
        //public Dictionary<string, string> ModuleDict { get; set; } = new Dictionary<string, string>();
        public List<TianCheng.Model.SelectView> ModuleDict { get; set; } = new List<TianCheng.Model.SelectView>();
    }
}

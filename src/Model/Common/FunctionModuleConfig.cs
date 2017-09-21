using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 模块的配置信息，用于读取写在appsettings.json中的信息
    /// </summary>
    public class FunctionModuleConfig
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> _ModuleDict = new Dictionary<string, string>();
        /// <summary>
        /// 获取模块的命名空间及模块名称
        /// </summary>
        public Dictionary<string, string> ModuleDict
        {
            get { return _ModuleDict; }
            set { _ModuleDict = value; }
        }
    }
}

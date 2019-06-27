using System.Collections.Generic;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 功能点分组
    /// </summary>
    public class FunctionGroupInfo
    {
        /// <summary>
        /// 功能点分类序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 功能点分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        public string Code { get; set; }

        private List<FunctionInfo> _FunctionList = new List<FunctionInfo>();
        /// <summary>
        /// 功能点列表
        /// </summary>
        public List<FunctionInfo> FunctionList
        {
            get { return _FunctionList; }
            set { _FunctionList = value; }
        }
        /// <summary>
        /// 所属模块编号
        /// </summary>
        public string ModeuleCode { get; set; }
    }
}

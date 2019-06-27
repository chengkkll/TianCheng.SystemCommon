using System.Collections.Generic;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 以树形结构显示区域信息
    /// </summary>
    public class AreaTree : AreaView
    {
        /// <summary>
        /// 下级行政机构
        /// </summary>
        public List<AreaTree> Sub { get; set; }
    }
}

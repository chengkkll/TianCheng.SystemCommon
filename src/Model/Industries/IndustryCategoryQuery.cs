using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 行业分类查询
    /// </summary>
    public class IndustryCategoryQuery : QueryInfo
    {
        /// <summary>
        /// 按行业分类名称模糊查询
        /// </summary>
        public string Name { get; set; }
    }
}

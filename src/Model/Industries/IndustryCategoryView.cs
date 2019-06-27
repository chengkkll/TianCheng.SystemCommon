using System.Collections.Generic;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 行业分类 查看对象
    /// </summary>
    public class IndustryCategoryView
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类中包含的行业信息
        /// </summary>
        public List<IndustryView> Industries { get; set; } = new List<IndustryView>();
    }
}

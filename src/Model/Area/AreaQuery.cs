namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 区域信息查询条件
    /// </summary>
    public class AreaQuery : TianCheng.Model.QueryInfo
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 按区域名称模糊查询
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 按区域编码查询
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 按上级区域ID查询
        /// </summary>
        public string SuperiorId { get; set; }
        /// <summary>
        /// 按上级区域Code查询
        /// </summary>
        public string SuperiorCode { get; set; }
        /// <summary>
        /// 按上级区域名称查询
        /// </summary>
        public string SuperiorName { get; set; }
        /// <summary>
        /// 按区域类型查询
        /// </summary>
        public AreaType AreaType { get; set; }
    }
}

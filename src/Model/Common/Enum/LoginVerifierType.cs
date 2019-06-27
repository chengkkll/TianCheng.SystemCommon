namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 用户登录类型
    /// </summary>
    public enum LoginVerifierType
    {
        /// <summary>
        ///  无特殊验证
        /// </summary>
        None = 0,
        /// <summary>
        /// 验证IP地址区域
        /// </summary>
        IpAddress = 1,
        /// <summary>
        /// 短期不验证
        /// </summary>
        ShortRange = 2,

    }
}

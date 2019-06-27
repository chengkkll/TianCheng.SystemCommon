namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// 正常可用状态
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 登录锁住
        /// </summary>
        LogonLock = 3,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 5
    }
}

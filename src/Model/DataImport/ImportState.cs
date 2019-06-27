namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 导入状态
    /// </summary>
    public enum ImportState
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 0,
        /// <summary>
        /// 已做数据转换
        /// </summary>
        Tran = 1,
        /// <summary>
        /// 检查数据
        /// </summary>
        Check = 2,
        /// <summary>
        /// 导入的数据有问题
        /// </summary>
        Fail = 4,
        /// <summary>
        /// 导入完成
        /// </summary>
        Complete = 8
    }
}

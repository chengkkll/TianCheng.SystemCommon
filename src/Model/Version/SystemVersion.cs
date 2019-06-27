using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 系统的版本信息
    /// </summary>
    public class SystemVersion : BusinessMongoModel
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 版本介绍
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 是否为当前版本
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}

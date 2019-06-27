using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 登录权限设置
    /// </summary>
    public class AuthSettingInfo : BusinessMongoModel
    {
        /// <summary>
        /// 登录时验证IP地址范围
        /// </summary>
        public string LoginVerifierIpAddress { get; set; }
    }
}

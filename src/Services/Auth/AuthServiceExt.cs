using TianCheng.BaseService;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 登录事件的扩展处理
    /// </summary>
    public class AuthServiceExt : IServiceExtOption
    {
        /// <summary>
        /// 设置扩展处理
        /// </summary>
        public void SetOption()
        {
            // 登录处理
            AuthService.OnLogin += LoginHistoryService.OnLogin;
        }
    }
}

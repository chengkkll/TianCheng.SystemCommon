using Newtonsoft.Json;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 登录返回结果
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}

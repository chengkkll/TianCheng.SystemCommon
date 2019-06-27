using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 登录参数
    /// </summary>
    public class LoginView
    {
        /// <summary>
        /// 账号
        /// </summary>
        [JsonProperty("account")]
        [Required(ErrorMessage = "账号不能为空")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}

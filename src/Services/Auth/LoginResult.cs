using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

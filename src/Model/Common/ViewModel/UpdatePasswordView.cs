using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 更新密码时的对象信息
    /// </summary>
    public class UpdatePasswordView
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        [JsonProperty("oldPwd")]
        public string OldPwd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [JsonProperty("newPwd")]
        public string NewPwd { get; set; }
    }
    /// <summary>
    /// 修改当前用户密码
    /// </summary>
    public class UpdatePasswordMeView
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [JsonProperty("oldPwd")]
        public string OldPwd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [JsonProperty("newPwd")]
        public string NewPwd { get; set; }
    }
}

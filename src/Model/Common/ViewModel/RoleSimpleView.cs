using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 简单的角色查看信息
    /// </summary>
    public class RoleSimpleView
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 显示的名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 当前角色登录后的默认页面
        /// </summary>
        [JsonProperty("page")]
        public string DefaultPage { get; set; }
    }
}

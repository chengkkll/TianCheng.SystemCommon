using Newtonsoft.Json;
using System;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginHistoryView
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 登录用户的ID
        /// </summary>
        public string LoginId { get; set; }
        /// <summary>
        /// 登录用户
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public string DepartmentID { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 请求IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("loginDate")]
        public DateTime CreateDate { get; set; }
    }
}

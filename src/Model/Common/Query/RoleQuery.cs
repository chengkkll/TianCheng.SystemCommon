using Newtonsoft.Json;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 角色的查询条件
    /// </summary>
    public class RoleQuery : TianCheng.Model.QueryInfo
    {
        /// <summary>
        /// 按名称模糊查询
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}

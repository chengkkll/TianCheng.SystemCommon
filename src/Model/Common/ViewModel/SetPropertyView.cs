using Newtonsoft.Json;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 按属性修改用户信息
    /// </summary>
    public class SetPropertyView
    {
        /// <summary>
        /// 属性名 
        /// </summary>
        [JsonProperty("property")]
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [JsonProperty("value")]
        public string PropertyValue { get; set; }

    }
}

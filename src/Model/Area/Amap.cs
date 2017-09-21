using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 高德返回行政区域信息
    /// </summary>
    public class Amap
    {
        /// <summary>
        /// 返回结果状态值     值为0或1，0表示失败；1表示成功
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
        /// <summary>
        /// 返回状态说明      返回状态说明，status为0时，info返回错误原因，否则返回“OK”。
        /// </summary>
        [JsonProperty("info")]
        public string Info { get; set; }

        /// <summary>
        /// 状态码         返回状态说明，1000代表正确，详情参阅info状态表
        /// </summary>
        [JsonProperty("infocode")]
        public string InfoCode { get; set; }


        private SuggestionInfo _Suggestion = new SuggestionInfo();
        /// <summary>
        /// 建议结果列表
        /// </summary>
        [JsonProperty("suggestion")]
        public SuggestionInfo Suggestion { get { return _Suggestion; } set { _Suggestion = value; } }


        private List<DistrictsInfo> _Districts = new List<DistrictsInfo>();
        /// <summary>
        /// 行政区列表
        /// </summary>
        public List<DistrictsInfo> Districts { get { return _Districts; } set { _Districts = value; } }
    }
    /// <summary>
    /// 建议结果列表
    /// </summary>
    public class SuggestionInfo
    {
        /// <summary>
        /// 建议关键字列表
        /// </summary>
        [JsonProperty("keywords")]
        public List<string> keywords { get; set; }
        /// <summary>
        /// 建议城市列表
        /// </summary>
        [JsonProperty("cites")]
        public List<string> cites { get; set; }
    }
    /// <summary>
    /// 行政区信息
    /// </summary>
    public class DistrictsInfo
    {
        /// <summary>
        /// 城市编码
        /// </summary>
        [JsonProperty("citycode")]
        public object CityCode { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [JsonProperty("adcode")]
        public string Adcode { get; set; }

        /// <summary>
        /// 行政区名称
        /// </summary>
        [JsonProperty("name")] 
        public string Name { get; set; }
        /// <summary>
        /// 行政区边界坐标点
        /// </summary>
        [JsonProperty("polyline")]
        public string Polyline { get; set; }
        /// <summary>
        /// 城市中心点
        /// </summary>
        [JsonProperty("center")]
        public string Center { get; set; }

        /// <summary>
        /// 行政区划级别
        /// </summary>
        /// <remarks>
        /// country:国家
        /// province:省份（直辖市会在province和city显示）
        /// city:市（直辖市会在province和city显示）
        /// district:区县
        /// street:街道
        /// </remarks>
        [JsonProperty("level")]
        public string Level { get; set; }

        /// <summary>
        /// 下级行政区列表
        /// </summary>
        [JsonProperty("districts")]
        public List<DistrictsInfo> Districts { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 高德返回行政区域信息
    /// </summary>
    public class AmapDistricts
    {
        /// <summary>
        /// 返回结果状态值     值为0或1，0表示失败；1表示成功
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 返回状态说明      返回状态说明，status为0时，info返回错误原因，否则返回“OK”。
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 状态码         返回状态说明，1000代表正确，详情参阅info状态表
        /// </summary>
        public string InfoCode { get; set; }

        /// <summary>
        /// 建议结果列表
        /// </summary>
        public SuggestionInfo Suggestion { get; set; } = new SuggestionInfo();

        /// <summary>
        /// 行政区列表
        /// </summary>
        public List<DistrictInfo> Districts { get; set; } = new List<DistrictInfo>();
    }
    /// <summary>
    /// 建议结果列表
    /// </summary>
    public class SuggestionInfo
    {
        /// <summary>
        /// 建议关键字列表
        /// </summary>
        public List<string> keywords { get; set; }
        /// <summary>
        /// 建议城市列表
        /// </summary>
        public List<string> cites { get; set; }
    }
    /// <summary>
    /// 行政区信息
    /// </summary>
    public class DistrictInfo
    {
        /// <summary>
        /// 城市编码
        /// </summary>
        public object CityCode { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public string CityCodeString
        {
            get
            {
                if (CityCode is String)
                {
                    return CityCode.ToString();

                }
                return string.Empty;
                //if (Level == "country" || Level == "province")
                //{
                //    return string.Empty;
                //}
            }
        }
        /// <summary>
        /// 区域编码    省份时传递[] 城市时传递编码
        /// </summary>
        public string Adcode { get; set; }

        /// <summary>
        /// 行政区名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 行政区边界坐标点
        /// </summary>
        public string Polyline { get; set; }
        /// <summary>
        /// 城市中心点
        /// </summary>
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
        public string Level { get; set; }

        /// <summary>
        /// 下级行政区列表
        /// </summary>
        public List<DistrictInfo> Districts { get; set; }
    }
}

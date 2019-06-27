using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 区域信息
    /// </summary>
    public class AreaInfo : BusinessMongoModel
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区域名称 英文
        /// </summary>
        public string NameEn { get; set; }
        /// <summary>
        /// 区域名称简写
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 区域邮编
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 电话区号
        /// </summary>
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 上级区域Id
        /// </summary>
        public string SuperiorId { get; set; }
        /// <summary>
        /// 上级区域Code
        /// </summary>
        public string SuperiorCode { get; set; }
        /// <summary>
        /// 上级区域名称
        /// </summary>
        public string SuperiorName { get; set; }

        /// <summary>
        /// 上级区域Id列表
        /// </summary>
        public List<string> SuperiorIds { get; set; } = new List<string>();
        /// <summary>
        /// 上级区域名称列表
        /// </summary>
        public List<string> SuperiorNames { get; set; } = new List<string>();
        /// <summary>
        /// 区域类型
        /// </summary>
        public AreaType AreaType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 区域类型
    /// </summary>
    public enum AreaType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 0,
        /// <summary>
        /// 国家
        /// </summary>
        Country = 1,
        /// <summary>
        /// 省 / 州
        /// </summary>
        Province = 2,
        /// <summary>
        /// 直辖市
        /// </summary>
        Municipality = 4,
        /// <summary>
        /// 城市
        /// </summary>
        City = 8,
        ///// <summary>
        ///// 县
        ///// </summary>
        //County = 16,
        ///// <summary>
        ///// 村 / 镇
        ///// </summary>
        //Town = 32,
    }
}

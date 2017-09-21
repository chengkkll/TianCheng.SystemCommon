﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    public class MenuSubInfo
    {
        /// <summary>
        /// 菜单序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 菜单的定位
        /// </summary>
        public string Sref { get; set; }
        /// <summary>
        /// 字体图标
        /// </summary>
        public string FontAwesome { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }
    }
}

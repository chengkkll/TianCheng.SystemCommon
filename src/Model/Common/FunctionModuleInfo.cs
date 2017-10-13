﻿using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL.MongoDB;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 功能模块
    /// </summary>
    [CollectionMapping("System_FunctionInfo")]
    public class FunctionModuleInfo : BusinessMongoModel
    {
        /// <summary>
        /// 模块序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模块编码
        /// </summary>
        public string Code { get; set; }

        private List<FunctionGroupInfo> _FunctionGroups = new List<FunctionGroupInfo>();
        /// <summary>
        /// 功能点分组（Control）
        /// </summary>
        public List<FunctionGroupInfo> FunctionGroups
        {
            get { return _FunctionGroups; }
            set { _FunctionGroups = value; }
        }
    }
}
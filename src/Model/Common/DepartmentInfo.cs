using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public class DepartmentInfo : BusinessMongoModel
    {
        #region 部门基本信息
        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排序的序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门描述
        /// </summary>
        public string Desc { get; set; }
        #endregion

        #region 上级信息
        /// <summary>
        /// 上级部门Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 上级部门ID列表
        /// </summary>
        public List<string> ParentsIds { get; set; } = new List<string>();
        #endregion

        #region 主管信息
        /// <summary>
        /// 部门主管ID
        /// </summary>
        public string ManageId { get; set; }
        /// <summary>
        /// 部门主管名称
        /// </summary>
        public string ManageName { get; set; }
        #endregion

        #region 扩展
        /// <summary>
        /// 扩展ID 用于部门信息的扩展
        /// </summary>
        public string ExtId { get; set; }
        #endregion
    }
}

using System.Collections.Generic;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 按部门分组的员工信息
    /// </summary>
    public class EmployeeGroupByDepartment
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 员工列表
        /// </summary>
        public List<SelectView> Employees { get; set; } = new List<SelectView>();
    }
}

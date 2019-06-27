namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 下拉列表查看员工的数据
    /// </summary>
    public class EmployeeSelectView
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 员工编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 是否在职
        /// </summary>
        public bool IsDelete { get; set; }
    }
}

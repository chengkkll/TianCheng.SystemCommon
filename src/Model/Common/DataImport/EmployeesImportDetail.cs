using TianCheng.Excel;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 员工信息导入明细信息
    /// </summary>
    [ExcelSheet("Employees")]
    public class EmployeesImportDetail : DataImportDetailInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [ExcelColumn("编号", "A")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [ExcelColumn("名称", "B")]
        public string Name { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        [ExcelColumn("登陆账号", "C")]
        public string LogonAccount { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [ExcelColumn("登录密码", "D")]
        public string LogonPassword { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [ExcelColumn("性别", "E")]
        public string Gender { get; set; }
        /// <summary>
        /// 是否在职
        /// </summary>
        [ExcelColumn("是否在职", "F")]
        public string IsHoldJob { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [ExcelColumn("部门", "G")]
        public string Department { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [ExcelColumn("角色", "H")]
        public string Role { get; set; }
    }
}

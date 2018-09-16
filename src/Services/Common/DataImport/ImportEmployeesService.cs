using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianCheng.BaseService;
using TianCheng.BaseService.Services;
using TianCheng.DataImport.Service;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.Company.Services
{
    /// <summary>
    /// 导入员工数据  服务处理
    /// </summary>
    public class ImportEmployeesService : DataImportService<EmployeesImportDetail, EmployeesImportDetailDAL, EmployeeInfo>
    {
        #region 属性设置
        /// <summary>
        /// 操作的数据类型标识
        /// </summary>
        protected override string DataType
        {
            get
            {
                return "Employees";
            }
        }
        #endregion

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        protected override EmployeeInfo Tran(EmployeesImportDetail import)
        {
            // 转换对象基本信息
            // 性别
            UserGender gender = UserGender.None;
            import.Gender = import.Gender.Trim();
            if (import.Gender.Contains("女") || import.Gender == "0" || import.Gender == "false")
            {
                gender = UserGender.Female;
            }
            if (import.Gender.Contains("男") || import.Gender == "1" || import.Gender == "true")
            {
                gender = UserGender.Male;
            }
            // 是否在职
            bool isDelete = false;
            import.IsHoldJob = import.IsHoldJob.Trim();
            if (import.IsHoldJob.Contains("是") || import.IsHoldJob == "1" || import.IsHoldJob == "true")
            {
                isDelete = false;
            }
            if (import.Gender.Contains("否") || import.Gender == "0" || import.Gender == "false")
            {
                isDelete = true;
            }
            EmployeeInfo info = new EmployeeInfo
            {
                OldId = import.Code,
                Code = import.Code,
                Name = import.Name,
                LogonAccount = import.LogonAccount,
                LogonPassword = import.LogonPassword,
                Gender = gender,
                IsDelete = isDelete,
                State = isDelete ? UserState.Disable : UserState.Enable
            };
            // 获取部门
            if (!String.IsNullOrWhiteSpace(import.Department))
            {
                DepartmentService depService = ServiceLoader.GetService<DepartmentService>();
                DepartmentInfo depInfo = depService.SearchQueryable().Where(e => e.Name == import.Department).FirstOrDefault();
                if (depInfo != null)
                {
                    info.Department = new SelectView { Id = depInfo.Id.ToString(), Name = depInfo.Name, Code = depInfo.Code };
                    info.ParentDepartment = new ParentDepartment { Id = depInfo.ParentId, Name = depInfo.ParentName, Ids = depInfo.ParentsIds };
                }
            }
            // 获取角色
            if (!String.IsNullOrWhiteSpace(import.Role))
            {
                RoleService roleService = ServiceLoader.GetService<RoleService>();
                RoleInfo roleInfo = roleService.SearchQueryable().Where(e => e.Name == import.Role).FirstOrDefault();
                if (roleInfo != null)
                {
                    info.Role = new SelectView { Id = roleInfo.Id.ToString(), Name = roleInfo.Name, Code = String.Empty };
                }
            }
            // 返回信息
            return info;
        }

        #region 导入数据
        /// <summary>
        /// 检查导入的数据
        /// </summary>
        /// <param name="import"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void ImportCheck(EmployeesImportDetail import, EmployeeInfo info)
        {
            string fail = String.Empty;
            if (String.IsNullOrEmpty(import.Code))
            {
                fail += "编号不能为空、";
            }
            if (String.IsNullOrEmpty(import.Name))
            {
                fail += "名称不能为空、";
            }
            if (String.IsNullOrEmpty(import.LogonAccount))
            {
                fail += "登陆账号不能为空、";
            }
            if (String.IsNullOrEmpty(import.LogonPassword))
            {
                fail += "登陆密码不能为空、";
            }

            if (!String.IsNullOrWhiteSpace(fail) || import.ImportState == DataImport.Model.ImportState.Fail)
            {
                import.ImportState = DataImport.Model.ImportState.Fail;
                import.FailureReason = fail;
                return;
            }
            else
            {
                import.ImportState = DataImport.Model.ImportState.Check;
                import.FailureReason = String.Empty;
            }
        }

        /// <summary>
        /// 排重的检查
        /// </summary>
        /// <param name="import"></param>
        /// <param name="info"></param>
        protected override void RepeatCheck(EmployeesImportDetail import, EmployeeInfo info)
        {
            string fail = String.Empty;
            // 判断是否已经导入过
            EmployeeService employeeService = ServiceLoader.GetService<EmployeeService>();
            if (!String.IsNullOrEmpty(import.Code) && employeeService.SearchQueryable().Where(e => e.Code == import.Code).Count() > 0)
            {
                fail += $"编码为{import.Code}的员工已经导入过，终止再次导入";
            }
            if (!String.IsNullOrEmpty(import.LogonAccount) && employeeService.SearchQueryable().Where(e => e.LogonAccount == import.LogonAccount).Count() > 0)
            {
                fail += $"编码为{import.Code}的{import.Name}员工登陆账号（{import.LogonAccount}）已存在，终止再次导入";
            }
            if (!String.IsNullOrEmpty(fail))
            {
                import.ImportState = DataImport.Model.ImportState.Fail;
                import.FailureReason = fail;
                return;
            }
            else
            {
                import.ImportState = DataImport.Model.ImportState.Check;
                import.FailureReason = String.Empty;
            }
        }

        /// <summary>
        /// 导入数据的具体操作
        /// </summary>
        /// <param name="import"></param>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void Import(EmployeesImportDetail import, EmployeeInfo info, TokenLogonInfo logonInfo)
        {
            EmployeeService employeeService = ServiceLoader.GetService<EmployeeService>();
            // 新增导入的数据
            employeeService.Create(info, logonInfo);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 版本更新时的数据库处理
    /// </summary>
    [DBMapping("system_version")]
    public class SystemVersionDAL : MongoOperation<SystemVersion>
    {
        /// <summary>
        /// 更新数据库的结构支撑V2.0的版本
        /// </summary>
        /// <remarks>原1.0的数据结构与2.0的不同</remarks>
        public void UpdateToV2()
        {
            // 修改集合名称
            IEnumerable<string> names = this.Provider.Database.ListCollectionNames().Current.ToList();
            if (names.Contains("System_DepartmentInfo"))
            {
                this.Provider.Database.RenameCollection("System_DepartmentInfo", "system_department");
            }
            if (names.Contains("System_EmployeeInfo"))
            {
                this.Provider.Database.RenameCollection("System_EmployeeInfo", "system_employee");
            }
            if (names.Contains("System_FunctionInfo"))
            {
                this.Provider.Database.RenameCollection("System_FunctionInfo", "system_function");
            }
            if (names.Contains("System_MenuInfo"))
            {
                this.Provider.Database.RenameCollection("System_MenuInfo", "system_menu");
            }
            if (names.Contains("System_RoleInfo"))
            {
                this.Provider.Database.RenameCollection("System_RoleInfo", "system_role");
            }

            // 修改菜单数据结构

            // 修改功能点的数据结构
        }
    }
}

using MongoDB.Driver;
using System;
using TianCheng.DAL;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 员工信息 [数据持久化]
    /// </summary>
    [DBMapping("system_employee")]
    public class EmployeeDAL : MongoOperation<EmployeeInfo>
    {
        /// <summary>
        /// 批量更新部门名称
        /// </summary>
        /// <param name="departmentInfo"></param>
        public void UpdateDepartmentInfo(DepartmentInfo departmentInfo)
        {
            // 验证参数
            if (departmentInfo.IsEmpty)
            {
                return;
            }
            // 设置查询条件
            FilterDefinition<EmployeeInfo> filter = Builders<EmployeeInfo>.Filter.Eq("Department.Id", departmentInfo.Id.ToString());
            // 设置更新内容
            UpdateDefinition<EmployeeInfo> ud = Builders<EmployeeInfo>.Update.Set("Department.Name", departmentInfo.Name)
                                                                             .Set("ParentDepartment.Id", departmentInfo.ParentId)
                                                                             .Set("ParentDepartment.Name", departmentInfo.ParentName)
                                                                             .Set("ParentDepartment.Ids", departmentInfo.ParentsIds);
            try
            {
                UpdateResult result = MongoCollection.UpdateMany(filter, ud);
            }
            catch (System.TimeoutException te)
            {
                DBLog.Logger.Warning(te, "数据库链接超时。链接字符串：" + Provider.Connection.ConnectionString());
                throw;
            }
            catch (Exception ex)
            {
                DBLog.Logger.Warning(ex, "操作异常终止。");
                throw;
            }
        }
        /// <summary>
        /// 批量更新角色名称
        /// </summary>
        /// <param name="roleInfo"></param>
        public void UpdateRoleInfo(RoleInfo roleInfo)
        {
            // 验证参数
            if (roleInfo.IsEmpty)
            {
                return;
            }
            // 设置查询条件
            FilterDefinition<EmployeeInfo> filter = Builders<EmployeeInfo>.Filter.Eq("Role.Id", roleInfo.Id.ToString());
            // 设置更新内容
            UpdateDefinition<EmployeeInfo> ud = Builders<EmployeeInfo>.Update.Set("Role.Name", roleInfo.Name);
            try
            {
                UpdateResult result = MongoCollection.UpdateMany(filter, ud);
            }
            catch (System.TimeoutException te)
            {
                DBLog.Logger.Warning(te, "数据库链接超时。链接字符串：" + Provider.Connection.ConnectionString());
                throw;
            }
            catch (Exception ex)
            {
                DBLog.Logger.Warning(ex, "操作异常终止。");
                throw;
            }
        }
    }
}

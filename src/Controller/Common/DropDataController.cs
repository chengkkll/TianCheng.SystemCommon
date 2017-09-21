using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.DAL;

namespace TianCheng.SystemCommon.Controller
{
    ///// <summary>
    ///// 清除数据操作
    ///// </summary>
    //[Produces("application/json")]
    //[Route("api/System")]
    //public class DropDataController
    //{       
    //    #region 系统用户信息
    //    /// <summary>
    //    /// 清除所有的系统用户信息、部门信息、角色信息
    //    /// </summary>
    //    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DropDataController.Employee")]
    //    [SwaggerOperation(Tags = new[] { "系统管理-数据清除" })]
    //    [Route("Employee")]
    //    [HttpDelete]
    //    public ResultView Employee()
    //    {
    //        DepartmentDAL departmentDal = new DepartmentDAL();
    //        int departmentCount = departmentDal.SearchQueryable().Count();
    //        departmentDal.Drop();

    //        EmployeeDAL employeeDal = new EmployeeDAL();
    //        int employeeCount = employeeDal.SearchQueryable().Count();
    //        employeeDal.Drop();

    //        RoleDAL roleDal = new RoleDAL();
    //        int roleCount = roleDal.SearchQueryable().Count();
    //        roleDal.Drop();

    //        return ResultView.Success($"共计" +
    //                                  $"{departmentCount}个部门信息被清除掉；" +
    //                                  $"{employeeCount}位员工信息被清除掉；" +
    //                                  $"{roleCount}个角色信息被清除掉。");
    //    }
    //    #endregion
    //}
}

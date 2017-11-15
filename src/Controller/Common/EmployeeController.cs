using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;
using TianCheng.BaseService.PlugIn.Swagger;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 员工管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : DataController
    {
        #region 构造方法
        private readonly EmployeeService _Service;
        private readonly ILogger<EmployeeController> _logger;
        private readonly RoleService _roleService;
        private readonly DepartmentService _departmentService;
        private readonly MenuService _menuService;

        /// <summary> 
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        /// <param name="roleService"></param>        
        /// <param name="departmentService"></param>   
        /// <param name="menuService"></param>   
        public EmployeeController(EmployeeService service, ILogger<EmployeeController> logger,
            RoleService roleService, DepartmentService departmentService, MenuService menuService)
        {
            _Service = service;
            _logger = logger;
            _roleService = roleService;
            _departmentService = departmentService;
            _menuService = menuService;
        }
        #endregion

        #region 获取当前用户权限信息
        /// <summary>
        /// 获取当前用户权限信息 没有此功能无法登陆
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Power")]
        [HttpGet("Power")]
        [SwaggerOperation(Tags = new[] { "登录验证相关接口" })]
        public LogonPowerView Power()
        {
            return Power(false);
        }

        /// <summary>
        /// 获取当前用户权限信息 没有此功能无法登陆
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Power")]
        [HttpGet("PowerSPA")]
        [SwaggerOperation(Tags = new[] { "登录验证相关接口" })]
        [HiddenApi]
        public LogonPowerView PowerSPA()
        {
            return Power(true);
        }
        /// <summary>
        /// 获取当前用户权限信息
        /// </summary>
        /// <param name="isSpa"></param>
        /// <returns></returns>
        private LogonPowerView Power(bool isSpa)
        {
            LogonPowerView view = new LogonPowerView();
            view.Id = LogonInfo.Id;
            view.Name = LogonInfo.Name;
            view.Role = new RoleSimpleView();
            view.Role.Id = LogonInfo.RoleId;
            var role = _roleService.SearchById(view.Role.Id);
            if (role != null)
            {
                view.Role.Name = role.Name;
                view.Role.DefaultPage = role.DefaultPage;
                //view.Menu = isSpa ? _menuService.ManageSingleTree() : role.PagePower;
                view.Menu = role.PagePower;
                view.Functions = role.FunctionPower;
            }
            view.Department = new BaseViewModel();
            view.Department.Id = LogonInfo.DepartmentId;
            if (!String.IsNullOrEmpty(view.Department.Id))
            {
                var dep = _departmentService._SearchById(view.Department.Id);
                if (dep != null)
                {
                    view.Department.Name = dep.Name;
                }
            }
            return view;
        }
        #endregion

        #region 新增修改数据
        /// <summary>
        /// 新增一个用户 
        /// </summary>
        /// <param name="view">请求体中放置新增对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("")]
        [HttpPost]
        public ResultView Create([FromBody]EmployeeView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改一个用户 
        /// </summary>
        /// <param name="view">请求体中带入修改对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("")]
        [HttpPut]
        public ResultView Update([FromBody]EmployeeView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        ///// <summary>
        ///// 修改昵称信息
        ///// </summary>
        ///// <param name="view">请求体中带入修改对象的信息</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.SetNickname")]
        //[SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        //[Route("SetNickname")]
        //[HttpPatch]
        //public ResultView SetNickname([FromBody]EmployeeView view)
        //{
        //    var info = _Service.SearchById(view.Id);
        //    info.Nickname = view.Nickname;
        //    return _Service.Update(info, LogonInfo);
        //}

        ///// <summary>
        ///// 修改职位信息
        ///// </summary>
        ///// <param name="view">请求体中带入修改对象的信息</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.SetPosition")]
        //[SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        //[Route("SetPosition")]
        //[HttpPatch]
        //public ResultView SetPosition([FromBody]EmployeeView view)
        //{
        //    var info = _Service.SearchById(view.Id);
        //    info.Position = view.Position;            
        //    return _Service.Update(info, LogonInfo);
        //}



        #endregion

        #region 数据删除
        /// <summary>
        /// 删除用户信息    
        /// </summary>
        /// <param name="id">要逻辑删除的对象id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("{id}")]
        [HttpDelete]
        public ResultView Delete(string id)
        {
            return _Service.Delete(id, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 根据ID获取一条用户信息    
        /// </summary>
        /// <param name="id">查询的用户ID</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.SearchById")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("{id}")]
        [HttpGet]
        public EmployeeView SearchById(string id)
        {
            return _Service.SearchById(id);
        }
        /// <summary>
        /// 获取当前用户信息    
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Load")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("")]
        [HttpGet]
        public EmployeeView Load()
        {
            string id = LogonInfo.Id;
            return _Service.SearchById(id);
        }

        /// <summary>
        /// 根据条件获取有分页信息的查询列表    
        /// </summary>
        /// <remarks> 
        /// 
        ///     排序规则包含： 
        /// 
        ///         nameDesc      : 按名称倒序排列          
        ///         nameAsc       : 按名称正序排列
        ///         depNameAsc    : 按部门名称正序排列
        ///         depNameDesc   : 按部门名称倒序排列
        ///         roleNameAsc   : 按角色名称正序排列
        ///         roleNameDesc  : 按角色名称倒叙排列
        ///         stateAsc      : 按状态正序排列
        ///         stateDesc     : 按状态倒叙排列
        ///         dateAsc       : 更新时间正序排列 
        ///         dateDesc      : 更新时间倒序排列     为默认排序
        ///         
        /// </remarks>
        /// <param name="queryInfo">查询信息。（包含分页信息、查询条件、排序条件）
        /// 排序规则参见上面的描述
        /// </param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.SearchPage")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("Search")]
        [HttpPost]
        public PagedResult<EmployeeView> SearchPage([FromBody]EmployeeQuery queryInfo)
        {
            var role = _roleService._SearchById(LogonInfo.RoleId);
            if (!role.Name.Contains("管理员"))
            {
                //非管理员只能查看自己部门下的用户
                queryInfo.RootDepartment = LogonInfo.DepartmentId;
            }

            return _Service.FilterPage(queryInfo);
        }

        /// <summary>
        /// 根据条件查询数据列表  无分页效果    
        /// </summary>
        /// <remarks> 
        /// 
        ///     排序规则包含： 
        /// 
        ///         nameDesc      : 按名称倒序排列          
        ///         nameAsc       : 按名称正序排列
        ///         depNameAsc    : 按部门名称正序排列
        ///         depNameDesc   : 按部门名称倒序排列
        ///         roleNameAsc   : 按角色名称正序排列
        ///         roleNameDesc  : 按角色名称倒叙排列
        ///         stateAsc      : 按状态正序排列
        ///         stateDesc     : 按状态倒叙排列
        ///         dateAsc       : 更新时间正序排列 
        ///         dateDesc      : 更新时间倒序排列     为默认排序
        ///         
        /// </remarks>
        /// <param name="queryInfo">查询信息。（包含查询条件、排序条件）
        /// 排序规则参见上面的描述
        /// </param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.SearchFilter")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("List")]
        [HttpPost]
        public List<EmployeeView> SearchFilter([FromBody]EmployeeQuery queryInfo)
        {
            return _Service.Filter(queryInfo);
        }

        /// <summary>
        /// 为下拉列表提供数据 - 获取所有的员工列表      
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("Select")]
        [HttpGet]
        public List<SelectView> Select()
        {
            EmployeeQuery query = new EmployeeQuery() { Pagination = QueryPagination.DefaultObject };
            query.Sort.Property = "name";
            return _Service.Select(query);
        }
        /// <summary>
        /// 为下拉列表提供数据 - 获取某部门下的员工列表   
        /// </summary>
        /// <param name="depId">部门id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.SelectByDepartment")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("SelectByDep/{depId}")]
        [HttpGet]
        public List<SelectView> SelectByDepartment(string depId)
        {
            EmployeeQuery query = new EmployeeQuery() { DepartmentId = depId, Pagination = QueryPagination.DefaultObject };
            query.Sort.Property = "name";
            return _Service.Select(query);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改其他用户密码
        /// </summary>
        /// <param name="view">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.UpdatePasswordOther")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("UpPwd/Other")]
        [HttpPatch]
        public ResultView UpdatePasswordOther([FromBody]UpdatePasswordView view)
        {
            return _Service.UpdatePassword(view.Id, view.OldPwd, view.NewPwd);
        }

        /// <summary>
        /// 修改当前用户密码
        /// </summary>
        /// <param name="view">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.UpdatePasswordMe")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("UpPwd/Me")]
        [HttpPatch]
        public ResultView UpdatePasswordMe([FromBody]UpdatePasswordMeView view)
        {
            _Service.UpdatePassword(LogonInfo.Id, view.OldPwd, view.NewPwd);
            return ResultView.Success("密码修改成功");
        }
        #endregion

        #region 员工状态控制
        /// <summary>
        /// 禁止某些员工登录系统，主要用于员工离职
        /// </summary>
        /// <param name="id">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Disable")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("Disable/{id}")]
        [HttpPatch]
        public ResultView Disable(string id)
        {
            return _Service.SetDisable(id);
        }

        /// <summary>
        /// 恢复某些员工禁止登录系统的状态
        /// </summary>
        /// <param name="id">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Enable")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("Enable/{id}")]
        [HttpPatch]
        public ResultView Enable(string id)
        {
            return _Service.SetEnable(id);
        }
        /// <summary>
        /// 解锁某些用户的登录状态 - 用户连续多次由于密码错误而登录失败时，将会为用户设置登录锁状态，本功能用于解除这种登录锁的状态
        /// </summary>
        /// <param name="id">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Unlock")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("Unlock/{id}")]
        [HttpPatch]
        public ResultView Unlock(string id)
        {
            return _Service.SetUnlock(id);
        }

        #endregion


        //#region 忘记密码

        //#endregion
    }
}

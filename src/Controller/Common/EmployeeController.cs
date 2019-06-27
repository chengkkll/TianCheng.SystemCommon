using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

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
        private readonly RoleService _roleService;
        private readonly DepartmentService _departmentService;

        /// <summary> 
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="roleService"></param>        
        /// <param name="departmentService"></param>   
        public EmployeeController(EmployeeService service, RoleService roleService, DepartmentService departmentService)
        {
            _Service = service;
            _roleService = roleService;
            _departmentService = departmentService;
        }
        #endregion

        #region 获取当前用户权限信息
        /// <summary>
        /// 获取当前用户权限信息
        /// </summary>
        /// <returns></returns>
        /// <power>登录</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Power")]
        [HttpGet("Power")]
        [SwaggerOperation(Tags = new[] { "系统管理-登录验证" })]
        public LogonPowerView Power()
        {
            LogonPowerView view = new LogonPowerView()
            {
                Id = LogonInfo.Id,
                Name = LogonInfo.Name,
                Role = new RoleSimpleView() { Id = LogonInfo.RoleId }
            };
            var role = _roleService.SearchById(view.Role.Id);
            if (role != null)
            {
                view.Role.Name = role.Name;
                view.Role.DefaultPage = role.DefaultPage;
                view.Menu = role.PagePower;
                view.Functions = role.FunctionPower;
            }
            view.Department = new BaseViewModel() { Id = LogonInfo.DepartmentId };
            if (!string.IsNullOrEmpty(view.Department.Id))
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
        /// 新增
        /// </summary>
        /// <param name="view">请求体中放置新增对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPost("")]
        public ResultView Create([FromBody]EmployeeView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="view">请求体中带入修改对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPut("")]
        public ResultView Update([FromBody]EmployeeView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        ///// <summary>
        ///// 修改昵称信息
        ///// </summary>
        ///// <param name="view">请求体中带入修改对象的信息</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.SetNickname")]
        //[SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        //[HttpPatch("SetNickname")]
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
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.SetPosition")]
        //[SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        //[HttpPatch("SetPosition")]
        //public ResultView SetPosition([FromBody]EmployeeView view)
        //{
        //    var info = _Service.SearchById(view.Id);
        //    info.Position = view.Position;            
        //    return _Service.Update(info, LogonInfo);
        //}
        #endregion

        #region 数据删除
        /// <summary>
        /// 设置离职    
        /// </summary>
        /// <param name="id">要逻辑删除的对象id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpDelete("Delete/{id}")]
        public ResultView Delete(string id)
        {
            return _Service.Delete(id, LogonInfo);
        }
        /// <summary>
        /// 设置在职    
        /// </summary>
        /// <param name="id">要逻辑删除的对象id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPatch("UnDelete/{id}")]
        public ResultView UnDelete(string id)
        {
            return _Service.UnDelete(id, LogonInfo);
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id">要逻辑删除的对象id</param>
        /// <power>粉碎数据</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Remove")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpDelete("Remove/{id}")]
        public ResultView Remove(string id)
        {
            return _Service.Remove(id, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 根据ID获取一条用户信息    
        /// </summary>
        /// <param name="id">查询的用户ID</param>
        /// <power>详情</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Single")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("{id}")]
        public EmployeeView SearchById(string id)
        {
            return _Service.SearchById(id);
        }
        /// <summary>
        /// 获取当前用户信息    
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Load")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("")]
        public EmployeeView Load()
        {
            return _Service.SearchById(LogonInfo.Id);
        }

        /// <summary>
        /// 根据条件获取有分页信息的查询列表    
        /// </summary>
        /// <remarks> 
        /// 
        ///     排序规则包含： 
        /// 
        ///         name            : 按名称排列          
        ///         department.name : 按部门名称排列
        ///         role.name       : 按角色名称倒序排列
        ///         state           : 按状态正序排列
        ///         updateDate      : 更新时间排列     为默认排序
        ///         
        /// </remarks>
        /// <param name="queryInfo">查询信息。（包含分页信息、查询条件、排序条件）
        /// 排序规则参见上面的描述
        /// </param>
        /// <power>查询</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPost("Search")]
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
        ///         name            : 按名称排列          
        ///         department.name : 按部门名称排列
        ///         role.name       : 按角色名称倒序排列
        ///         state           : 按状态正序排列
        ///         updateDate      : 更新时间排列     为默认排序
        ///         
        /// </remarks>
        /// <param name="queryInfo">查询信息。（包含查询条件、排序条件）
        /// 排序规则参见上面的描述
        /// </param>
        /// <power>查询</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPost("List")]
        public List<EmployeeView> SearchFilter([FromBody]EmployeeQuery queryInfo)
        {
            return _Service.Filter(queryInfo);
        }

        /// <summary>
        /// 为下拉列表提供数据 - 获取所有的员工列表      
        /// </summary>
        /// <power>列表选择</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("Select")]
        public List<SelectView> Select()
        {
            EmployeeQuery query = new EmployeeQuery() { Pagination = QueryPagination.DefaultObject };
            query.Sort.Property = "name";
            return _Service.Select(query);
        }
        /// <summary>
        /// 为下拉列表提供数据 - 获取所有的员工列表      
        /// </summary>
        /// <power>列表选择</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("SelectView")]
        public List<EmployeeSelectView> SelectDepartment()
        {
            List<EmployeeSelectView> viewList = new List<EmployeeSelectView>();
            foreach (var info in _Service.SearchQueryable().OrderBy(e => e.Name).ToList())
            {
                viewList.Add(new EmployeeSelectView
                {
                    Id = info.Id.ToString(),
                    Name = info.Name,
                    Code = info.Code,
                    DepartmentId = info.Department.Id,
                    DepartmentName = info.Department.Name,
                    RoleId = info.Role.Id,
                    RoleName = info.Role.Name,
                    IsDelete = info.IsDelete
                });
            }
            return viewList;
        }
        /// <summary>
        /// 为下拉列表提供数据 - 获取某部门下的员工列表（不显示已离职的员工）   
        /// </summary>
        /// <param name="depId">部门id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.SelectByDepartment")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("SelectByDep/{depId}")]
        public List<SelectView> SelectByDepartment(string depId)
        {
            EmployeeQuery query = new EmployeeQuery() { DepartmentId = depId, Pagination = QueryPagination.DefaultObject, HasDelete = 0 };
            query.Sort.Property = "name";
            return _Service.Select(query);
        }
        /// <summary>
        /// 为下拉列表提供数据 - 获取某部门下的员工列表（显示已离职的员工）   
        /// </summary>
        /// <param name="depId">部门id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.SelectByDepartment")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("SelectByDep/{depId}/All")]
        public List<SelectView> SelectByDepartmentAll(string depId)
        {
            EmployeeQuery query = new EmployeeQuery() { DepartmentId = depId, Pagination = QueryPagination.DefaultObject, HasDelete = 1 };
            query.Sort.Property = "name";
            return _Service.Select(query);
        }
        /// <summary>
        /// 为下拉列表提供数据 - 获取某部门下的员工列表   
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.SelectByDepartment")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("SelectMyDepartment")]
        public List<SelectView> SelectByMyDepartment()
        {
            EmployeeQuery query = new EmployeeQuery() { DepartmentId = LogonInfo.DepartmentId, Pagination = QueryPagination.DefaultObject };
            query.Sort.Property = "name";
            return _Service.Select(query);
        }

        /// <summary>
        /// 获取所有可用的员工列表，按部门分组
        /// </summary>
        /// <returns></returns>
        /// <power>列表选择</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpGet("SelectGroupByDepartment")]
        public List<EmployeeGroupByDepartment> GetEmployeeByDepartment()
        {
            return _Service.GetEmployeeByDepartment();
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改其他用户密码
        /// </summary>
        /// <param name="view">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.UpdatePasswordOther")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPatch("UpPwd/Other")]
        public ResultView UpdatePasswordOther([FromBody]UpdatePasswordView view)
        {
            return _Service.UpdatePassword(view.Id, view.OldPwd, view.NewPwd);
        }

        /// <summary>
        /// 修改当前用户密码
        /// </summary>
        /// <param name="view">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.UpdatePasswordMe")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPatch("UpPwd/Me")]
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
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Disable")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPatch("Disable/{id}")]
        public ResultView Disable(string id)
        {
            return _Service.SetDisable(id);
        }

        /// <summary>
        /// 恢复某些员工禁止登录系统的状态
        /// </summary>
        /// <param name="id">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Enable")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPatch("Enable/{id}")]
        public ResultView Enable(string id)
        {
            return _Service.SetEnable(id);
        }
        /// <summary>
        /// 解锁某些用户的登录状态 - 用户连续多次由于密码错误而登录失败时，将会为用户设置登录锁状态，本功能用于解除这种登录锁的状态
        /// </summary>
        /// <param name="id">用户id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Unlock")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPatch("Unlock/{id}")]
        public ResultView Unlock(string id)
        {
            return _Service.SetUnlock(id);
        }

        #endregion


        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="employeeId">请求体中带入修改对象的信息</param>
        /// <param name="imageFile">上传的文件信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.SetHeadImg")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("{employeeId}/SetHeadImg")]
        [HttpPatch]
        public ResultView SetHeadImg(string employeeId, Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            string webFile = $"~/wwwroot/UploadFiles/Employee/Head/{employeeId}/{Guid.NewGuid().ToString()}";
            UploadFileInfo file = UploadFileHandle.UploadImage(webFile, imageFile);

            var info = _Service._SearchById(employeeId);
            UploadFileHandle.Delete(info.HeadImg);
            info.HeadImg = file.WebFileName;
            _Service.Update(info, LogonInfo);
            return ResultView.Success("头像修改成功");
        }

        /// <summary>
        /// 按属性修改用户信息
        /// </summary>
        /// <remarks> 
        /// 
        ///     property包含（不区分大小写）： 
        /// 
        ///         Position      : 职位          
        ///         Name          : 名称 
        ///         Nickname      : 昵称
        ///         Mobile        : 手机电话
        ///         Telephone     : 座机电话
        ///         
        /// </remarks>
        /// <param name="view"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Employee.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("SetProperty")]
        [HttpPatch]
        public ResultView SetProperty([FromBody] SetPropertyView view)
        {
            if (string.IsNullOrWhiteSpace(view.PropertyName))
            {
                ApiException.ThrowBadRequest("属性名不能为空");
            }

            var info = _Service._SearchById(LogonInfo.Id);
            switch (view.PropertyName.ToLower())
            {
                case "position": { info.Position = view.PropertyValue; break; }
                case "name": { info.Name = view.PropertyValue; break; }
                case "nickname": { info.Nickname = view.PropertyValue; break; }
                case "mobile": { info.Mobile = view.PropertyValue; break; }
                case "telephone": { info.Telephone = view.PropertyValue; break; }
            }
            _Service.Update(info, LogonInfo);
            return ResultView.Success("信息修改成功");
        }

        /// <summary>
        /// 更新所有的人的密码
        /// </summary>
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [HttpPatch("UpdateAllPassword")]
        public ResultView UpdateAllPassword()
        {
            _Service.UpdateAllPassword("123456");
            return ResultView.Success();
        }

        //#region 忘记密码

        //#endregion
    }
}

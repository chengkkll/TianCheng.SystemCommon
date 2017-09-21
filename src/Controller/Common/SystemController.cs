using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 系统控制
    /// </summary>
    [Route("api/System")]
    public class SystemController : DataController
    {
        #region 构造方法
        private readonly EmployeeService _EmployeeService;
        private readonly FunctionService _FunctionService;
        private readonly MenuService _MenuService;
        private readonly RoleService _RoleService;
        private readonly ILogger<SystemController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="functionService"></param>
        /// <param name="menuService"></param>
        /// <param name="roleService"></param>
        /// <param name="logger"></param>        
        public SystemController(EmployeeService employeeService, FunctionService functionService,
            MenuService menuService, RoleService roleService, ILogger<SystemController> logger)
        {
            _EmployeeService = employeeService;
            _FunctionService = functionService;
            _MenuService = menuService;
            _RoleService = roleService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// 初始化系统数据
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.SystemController.Init")]
        [SwaggerOperation(Tags = new[] { "系统管理-系统设置" })]
        [Route("Init")]
        [HttpPost]
        public void Init()
        {
            //初始化系统菜单
            _MenuService.Init();
            //初始化系统功能点列表
            _FunctionService.Init();
            //初始化角色信息
            _RoleService.InitAdmin();
            //初始化用户信息
            _EmployeeService.UpdateAdmin();
        }

        /// <summary>
        /// 更新Admin账号信息
        /// </summary>
        /// <param name="updatePwd"></param>
        [SwaggerOperation(Tags = new[] { "系统管理-系统设置" })]
        [Route("UpdateAdmin/{updatePwd}")]
        [HttpGet]
        public void UpdateAdmin(string updatePwd)
        {
            if (updatePwd != "ua_`12")
            {
                return;
            }

            Init();

        }
    }
}

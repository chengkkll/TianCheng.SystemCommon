using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Services;

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
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="functionService"></param>
        /// <param name="menuService"></param>
        /// <param name="roleService"></param>
        public SystemController(EmployeeService employeeService, FunctionService functionService, MenuService menuService, RoleService roleService)
        {
            _EmployeeService = employeeService;
            _FunctionService = functionService;
            _MenuService = menuService;
            _RoleService = roleService;
        }
        #endregion

        /// <summary>
        /// 重置数据库
        /// </summary>
        /// <remarks>清除已有数据，初始成默认数据。/r/n初始化数据包括：菜单、功能点、角色、用户</remarks>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.System.Init")]
        [SwaggerOperation(Tags = new[] { "系统管理-系统设置" })]
        [HttpPost("InitDB")]
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
        /// 初始化数据库
        /// </summary>
        /// <remarks>无需登录的初始化数据库，必须系统中无用户信息时才可执行</remarks>
        [SwaggerOperation(Tags = new[] { "系统管理-系统设置" })]
        [HttpPost("Init")]
        public void UpdateAdmin()
        {
            var employeeService = ServiceLoader.GetService<EmployeeService>();
            if (employeeService.SearchQueryable().Count() > 0)
            {
                ApiException.ThrowBadRequest("系统中拥有用户信息，无密码初始化失败。您可以通过管理员账号登陆后再初始化");
            }
            Init();
        }

        ///// <summary>
        ///// 备份数据库
        ///// </summary>
        //[SwaggerOperation(Tags = new[] { "系统管理-系统设置" })]
        //[HttpPost("MongoDB/bak")]
        //public void MongoDBBak()
        //{
        //    var psi = new ProcessStartInfo("shell\\BakMongoDB.bat");
        //    //启动
        //    var proc = Process.Start(psi);
        //}
    }
}

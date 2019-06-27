using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using TianCheng.BaseService;
using TianCheng.BaseService.PlugIn;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 登录处理
    /// </summary>
    public class AuthService : IAuthService
    {
        #region 构造方法
        private readonly EmployeeDAL _employeeDal;
        private readonly ILogger<AuthService> _logger;
        private readonly TokenProviderOptions _tokenOptions;
        EmployeeService _EmployeeService;
        RoleService _RoleService;
        MenuService _MenuService;
        FunctionService _FunctionService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="employeeDal"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="employeeService"></param>
        /// <param name="roleService"></param>
        /// <param name="menuService"></param>
        /// <param name="functionService"></param>
        public AuthService(EmployeeDAL employeeDal, ILogger<AuthService> logger,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            EmployeeService employeeService, RoleService roleService, MenuService menuService, FunctionService functionService)
        {
            _employeeDal = employeeDal;
            _logger = logger;
            _EmployeeService = employeeService;
            _RoleService = roleService;
            _MenuService = menuService;
            _FunctionService = functionService;
            _tokenOptions = TokenProviderOptions.Load(configuration);
        }
        #endregion

        /// <summary>
        /// 每次请求接口时，加载用户的权限信息
        /// </summary>
        /// <param name="identity"></param>
        public void FillFunctionPolicy(ClaimsIdentity identity)
        {
            string userId = "";
            foreach (var item in identity.Claims)
            {
                if (item.Type == "id")
                {
                    userId = item.Value;
                    break;
                }
            }


            EmployeeView employee = _EmployeeService.SearchById(userId);

            // 添加角色声明
            //Claim claimRole = new Claim(ClaimTypes.Role, employee.Role.name);
            //identity.AddClaim(claimRole);
            if (employee == null)
            {
                throw ApiException.BadRequest("无法根据Token信息获取用户");
            }
            // 添加权限声明
            RoleView role = _RoleService.SearchById(employee.Role.Id);
            if (role == null)
            {
                throw ApiException.BadRequest("无法根据用户获取角色信息");
            }
            if (role.FunctionPower == null)
            {
                //如果功能点为空，初始化功能点
                if (_FunctionService.SearchQueryable().Count() == 0)
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
            }
            if (role.FunctionPower.Count == 0)
            {
                _logger.LogInformation($"{employee.Name}用户无任何功能点权限。所属角色为：{role.Name}({role.Id.ToString()})");
            }
            foreach (var function in role.FunctionPower)
            {
                identity.AddClaim(new Claim("function_policy", function.Policy));
            }
        }

        /// <summary>
        /// 登录时的事件处理
        /// </summary>
        static public Action<EmployeeInfo> OnLogin;
        /// <summary>
        /// 退出时的事件处理
        /// </summary>
        static public Action<TokenLogonInfo> OnLogout;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string account, string password)
        {
            account = account.Trim();
            password = password.Trim();
            //查询密码正确的可用用户列表
            var pwdQuery = _employeeDal.Queryable().Where(e => e.LogonPassword == password && e.IsDelete == false).ToList();

            // 通过账号和密码验证登录
            var employeeList = pwdQuery.Where(e => e.LogonAccount == account);
            if (employeeList.Count() > 1)
            {
                throw ApiException.BadRequest("有多个满足条件的用户，无法登陆。");
            }

            // 如果查找不到用户信息，并且允许用电话登录，尝试电话号码+登录密码登录
            if (employeeList.Count() == 0 && AuthServiceOption.Option.IsLogonByTelephone)
            {
                employeeList = pwdQuery.Where(e => e.Telephone == account || e.Mobile == account);
                if (employeeList.Count() > 1)
                {
                    throw ApiException.BadRequest("有多个满足条件的用户，无法通过电话登陆。");
                }
            }

            var employee = employeeList.FirstOrDefault();

            if (employee == null)
            {
                throw ApiException.BadRequest("您的登陆账号或密码错误。");
            }

            if (employee.State == UserState.Disable)
            {
                throw ApiException.BadRequest("您的登陆功能已被禁用，请与管理员联系。");
            }
            if (employee.State == UserState.LogonLock)
            {
                throw ApiException.BadRequest("多次登陆失败，登陆已被锁住，请与管理员联系。");
            }

            // 执行扩展的登录事件
            OnLogin?.Invoke(employee);

            List<Claim> claims = new List<Claim>
            {
                new Claim("id",employee.Id.ToString()),
                new Claim("name", employee.Name ?? ""),
                new Claim("roleId",employee.Role?.Id ?? ""),
                new Claim("depId",employee.Department?.Id ?? ""),
                new Claim("depName",employee.Department?.Name ?? ""),
                new Claim("type","AuthService")
            };


            var identity = new ClaimsIdentity(new GenericIdentity(account, "Token"), claims);
            string token = Jwt.GenerateJwtToken(account, identity, _tokenOptions);
            return token;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logonInfo"></param>
        /// <returns></returns>
        public void Logout(TokenLogonInfo logonInfo)
        {
            OnLogout?.Invoke(logonInfo);
        }
    }

}

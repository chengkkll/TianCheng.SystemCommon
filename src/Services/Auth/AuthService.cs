using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
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
    public class AuthService : IServiceRegister, IAuthService
    {
        #region 构造方法
        private readonly EmployeeDAL _employeeDal;
        private readonly ILogger<AuthService> _logger;
        private readonly TokenProviderOptions _tokenOptions;
        EmployeeService _EmployeeService;
        RoleService _RoleService;
        MenuService _MenuService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="employeeDal"></param>
        /// <param name="logger"></param>
        /// <param name="tokenOptions"></param>
        /// <param name="employeeService"></param>
        /// <param name="roleService"></param>
        /// <param name="menuService"></param>
        public AuthService(EmployeeDAL employeeDal, ILogger<AuthService> logger, IOptions<TokenProviderOptions> tokenOptions,
            EmployeeService employeeService, RoleService roleService, MenuService menuService)
        {
            _employeeDal = employeeDal;
            _logger = logger;
            _tokenOptions = tokenOptions.Value;
            _EmployeeService = employeeService;
            _RoleService = roleService;
            _MenuService = menuService;
        }
        #endregion

        /// <summary>
        /// 每次请求接口时，加载用户的权限信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="identity"></param>
        public void FillFunctionPolicy(string userId, ClaimsIdentity identity)
        {
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
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string account, string password)
        {
            account = account.Trim();
            password = password.Trim();

            var employeeList = _employeeDal.SearchQueryable()
                .Where(e => e.LogonAccount == account && e.LogonPassword == password && e.IsDelete == false);
            if (employeeList.Count() > 1)
            {
                throw ApiException.BadRequest("有多个满足条件的用户，无法登陆。");
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

            List<Claim> claims = new List<Claim>
            {
                new Claim("id",employee.Id.ToString()),
                new Claim("name", employee.Name ?? ""),
                new Claim("roleId",employee.Role?.Id ?? ""),
                new Claim("depId",employee.Department?.Id ?? "")
            };

            var identity = new ClaimsIdentity(new GenericIdentity(account, "Token"), claims);
            string token = Jwt.GenerateJwtToken(employee.Id.ToString(), identity, _tokenOptions);
            return token;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using TianCheng.BaseService;
using TianCheng.BaseService.PlugIn;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 游客权限设置
    /// </summary>
    public class AuthVisitorService : IAuthService
    {
        #region 构造方法
        private readonly TokenProviderOptions _tokenOptions;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        public AuthVisitorService(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _tokenOptions = TokenProviderOptions.Load(configuration);
        }
        #endregion

        #region 访问接口时 完善用户权限
        /// <summary>
        /// 访问接口时 获取用户的角色信息
        /// </summary>
        static public Func<ClaimsIdentity, RoleView> OnLoadRole;

        /// <summary>
        /// 访问接口时 完善用户权限
        /// </summary>
        /// <param name="identity"></param>
        public void FillFunctionPolicy(ClaimsIdentity identity)
        {
            RoleView role = new RoleView();
            // 如果有获取角色的委托方法，使用委托方法
            if (OnLoadRole != null)
            {
                role = OnLoadRole(identity);
            }
            else
            {
                // 暂不添加游客的角色获取逻辑
            }

            // 添加权限声明
            if (role == null)
            {
                throw ApiException.BadRequest("无法根据用户获取角色信息");
            }
            if (role.FunctionPower.Count == 0)
            {
                throw ApiException.BadRequest("您没有任何权限，请与管理员联系");
            }
            foreach (var function in role.FunctionPower)
            {
                identity.AddClaim(new Claim("function_policy", function.Policy));
            }
        }
        #endregion

        #region 登录处理
        /// <summary>
        /// 登录时的事件处理
        /// </summary>
        static public Func<LoginView, List<Claim>> OnLogin;
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
            List<Claim> claims = new List<Claim>();
            // 如果有登录的委托方法，使用委托方法
            if (OnLogin != null)
            {
                claims = OnLogin(new LoginView { Account = account, Password = password });
            }
            else
            {
                // 暂不添加游客的登录处理
            }

            claims.Add(new Claim("type", "AuthVisitorService"));

            var identity = new ClaimsIdentity(new GenericIdentity(account, "Token"), claims);
            string token = Jwt.GenerateJwtToken(account, identity, _tokenOptions);
            return token;
        }
        #endregion

        #region 退出处理
        /// <summary>
        /// 退出时的事件处理
        /// </summary>
        static public Action<TokenLogonInfo> OnLogout;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logonInfo"></param>
        /// <returns></returns>
        public void Logout(TokenLogonInfo logonInfo)
        {
            OnLogout?.Invoke(logonInfo);
        }
        #endregion
    }
}

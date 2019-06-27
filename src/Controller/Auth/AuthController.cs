using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 系统控制
    /// </summary>
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : DataController
    {
        #region 构造方法
        private readonly AuthService _authService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        #endregion

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="loginView"></param>
        /// <response code="200">登录成功返回token</response>
        /// <returns></returns>
        [HttpPost("login")]
        [SwaggerOperation(Tags = new[] { "系统管理-登录验证" })]
        public LoginResult Login([FromBody]LoginView loginView)
        {
            string token = _authService.Login(loginView.Account, loginView.Password);
            return new LoginResult { Token = token };
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [SwaggerOperation(Tags = new[] { "系统管理-登录验证" })]
        public ResultView Logout()
        {
            _authService.Logout(LogonInfo);
            return ResultView.Success();
        }
    }
}
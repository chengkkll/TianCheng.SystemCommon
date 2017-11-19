using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using TianCheng.BaseService.PlugIn;

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
        private readonly IAuthService _authService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(IAuthService authService)
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
        [SwaggerOperation(Tags = new[] { "登录验证相关接口" })]
        public LoginResult Login([FromBody]LoginView loginView)
        {
            string token = _authService.Login(loginView.Account, loginView.Password);
            return new LoginResult { Token = token };
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.SystemController.Power")]
        [HttpPost("logout")]
        [SwaggerOperation(Tags = new[] { "登录验证相关接口" })]
        public ResultView Logout()
        {
            _authService.Logout(LogonInfo);
            return ResultView.Success();
        }
    }
}
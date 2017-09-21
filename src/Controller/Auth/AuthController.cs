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

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 登录处理Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Microsoft.AspNetCore.Mvc.Controller
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
        [HttpPost("Login")]
        [SwaggerOperation(Tags = new[] { "登录验证相关接口" })]
        public LoginResult Login([FromBody]LoginView loginView)
        {
            string token = _authService.Login(loginView.Account, loginView.Password);
            return new LoginResult { Token = token };
        }
    }
}
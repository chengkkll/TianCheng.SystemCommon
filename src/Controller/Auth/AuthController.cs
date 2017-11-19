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
    /// ϵͳ����
    /// </summary>
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : DataController
    {
        #region ���췽��
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
        /// ��¼�ӿ�
        /// </summary>
        /// <param name="loginView"></param>
        /// <response code="200">��¼�ɹ�����token</response>
        /// <returns></returns>
        [HttpPost("login")]
        [SwaggerOperation(Tags = new[] { "��¼��֤��ؽӿ�" })]
        public LoginResult Login([FromBody]LoginView loginView)
        {
            string token = _authService.Login(loginView.Account, loginView.Password);
            return new LoginResult { Token = token };
        }

        /// <summary>
        /// �˳���¼
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.SystemController.Power")]
        [HttpPost("logout")]
        [SwaggerOperation(Tags = new[] { "��¼��֤��ؽӿ�" })]
        public ResultView Logout()
        {
            _authService.Logout(LogonInfo);
            return ResultView.Success();
        }
    }
}
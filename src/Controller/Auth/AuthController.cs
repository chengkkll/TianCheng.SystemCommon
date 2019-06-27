using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Services;

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
        /// ��¼�ӿ�
        /// </summary>
        /// <param name="loginView"></param>
        /// <response code="200">��¼�ɹ�����token</response>
        /// <returns></returns>
        [HttpPost("login")]
        [SwaggerOperation(Tags = new[] { "ϵͳ����-��¼��֤" })]
        public LoginResult Login([FromBody]LoginView loginView)
        {
            string token = _authService.Login(loginView.Account, loginView.Password);
            return new LoginResult { Token = token };
        }

        /// <summary>
        /// �˳���¼
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [SwaggerOperation(Tags = new[] { "ϵͳ����-��¼��֤" })]
        public ResultView Logout()
        {
            _authService.Logout(LogonInfo);
            return ResultView.Success();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Services;
using TianCheng.SystemCommon.Services.Auth;

namespace TianCheng.SystemCommon.Controller.Auth
{
    /// <summary>
    /// 部门IP
    /// </summary>
    /// <power>系统控制</power>
    [Produces("application/json")]
    [Route("api/auth/departmentIp")]
    public class DepartmentIpController : DataController
    {
        #region 构造方法
        private readonly DepartmentIpService _Service;
        private readonly ILogger<DepartmentIpController> _logger;

        /// <summary> 
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public DepartmentIpController(DepartmentIpService service, ILogger<DepartmentIpController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// 新增部门IP
        /// </summary>
        /// <param name="id">部门id</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [SwaggerOperation(Tags = new[] { "系统管理-登录验证" })]
        public ResultView Login(string id)
        {
            DepartmentService departmentService = ServiceLoader.GetService<DepartmentService>();
            var dep = departmentService._SearchById(id);
            return ResultView.Success();
            //return new LoginResult { Token = token };
        }
    }
}

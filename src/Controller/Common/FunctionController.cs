using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 功能点管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Function")]
    public class FunctionController : DataController
    {
        #region 构造方法
        private readonly FunctionService _Service;
        private readonly ILogger<FunctionController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public FunctionController(FunctionService service, ILogger<FunctionController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 查询所有的功能点信息，以树形结构显示，无分页信息      
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.FunctionController.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("")]
        [HttpGet]
        public List<FunctionModuleView> Search()
        {
            return _Service.LoadTree();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化功能点   清除已有功能点，重新设置默认功能点
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.FunctionController.Init")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("Init")]
        [HttpPost]
        public ResultView Init()
        {
            _Service.Init();

            return ResultView.Success();
        }
        #endregion
    }
}

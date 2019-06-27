using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.BaseService.PlugIn.Swagger;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 员工管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeImageController : DataController
    {
        #region 构造方法
        private readonly EmployeeService _Service;
        private readonly ILogger<EmployeeImageController> _logger;

        /// <summary> 
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public EmployeeImageController(EmployeeService service, ILogger<EmployeeImageController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

    }
}

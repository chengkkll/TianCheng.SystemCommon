using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 系统控制
    /// </summary>
    [Produces("application/json")]
    [Route("api/Auotrun/Record")]
    public class TimerRecordController : DataController
    {
        #region 构造方法
        private readonly TimerRecordService _Service;
        private readonly ILogger<TimerRecordController> _logger;

        /// <summary> 
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public TimerRecordController(TimerRecordService service, ILogger<TimerRecordController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        /// <power>查询定时运行结果</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Auotrun.SearchPage")]
        [SwaggerOperation(Tags = new[] { "系统管理-定时运行" })]
        [Route("Search")]
        [HttpPost]
        public PagedResult<TimerRecordView> SearchPage([FromBody]TimerRecordQuery queryInfo)
        {
            return _Service.FilterPage(queryInfo);
        }
    }
}

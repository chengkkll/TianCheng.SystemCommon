using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 版本控制
    /// </summary>
    [Route("api/Version")]
    public class SystemVersionController : DataController
    {
        #region 构造方法
        /// <summary>
        /// 版本操作服务
        /// </summary>
        private readonly SystemVersionService Service;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="systemVersionService"></param>
        public SystemVersionController(SystemVersionService systemVersionService)
        {
            Service = systemVersionService;
        }
        #endregion

        /// <summary>
        /// 更新数据库的结构支撑V2.0的版本
        /// </summary>
        /// <remarks>原1.0的数据结构与2.0的不同</remarks>
        [SwaggerOperation(Tags = new[] { "系统管理-版本控制" })]
        [HttpPost("UpdateToV2")]
        public ResultView UpdateToV2()
        {
            Service.UpdateToV2();
            return ResultView.Success();
        }
    }
}

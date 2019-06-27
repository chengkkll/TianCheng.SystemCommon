using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
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
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        public FunctionController(FunctionService service)
        {
            _Service = service;
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 查询功能点
        /// </summary>
        /// <remarks>查询所有的功能点信息，以树形结构显示结果，无分页信息</remarks>
        /// <power>查询</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Function.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpGet("")]
        public List<FunctionModuleView> Search()
        {
            return _Service.LoadTree();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <remarks>初始化功能点。   清除已有功能点，分析引用项目的注释信息来重置功能点</remarks>
        /// <power>初始化</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Function.Init")]
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

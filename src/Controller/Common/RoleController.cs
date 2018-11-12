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
    /// 角色管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Role")]
    public class RoleController : DataController
    {
        #region 构造方法
        private readonly RoleService _Service;
        private readonly ILogger<RoleController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public RoleController(RoleService service, ILogger<RoleController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        #region 新增修改数据
        /// <summary>
        /// 新增一个角色 
        /// </summary>
        /// <param name="view">请求体中放置新增角色的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.RoleController.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [Route("")]
        [HttpPost]
        public ResultView Create([FromBody]RoleView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改一个角色    
        /// </summary>
        /// <param name="view">请求体中带入修改角色的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.RoleController.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [Route("")]
        [HttpPut]
        public ResultView Update([FromBody]RoleView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        #endregion

        #region 数据删除
        /// <summary>
        /// 删除一个角色       
        /// </summary>
        /// <param name="id">要删除的角色id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.RoleController.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [Route("{id}")]
        [HttpDelete]
        public ResultView Delete(string id)
        {
            return _Service.Delete(id, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 根据ID获取一个角色信息      
        /// </summary>
        /// <param name="id">要获取的对象ID</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.RoleController.SearchById")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [Route("{id}")]
        [HttpGet]
        public RoleView SearchById(string id)
        {
            return _Service.SearchById(id);
        }

        /// <summary>
        /// 查询角色列表（分页 + 查询条件）      
        /// </summary>
        /// <remarks> 
        /// 
        ///     排序规则包含： 
        /// 
        ///         name            : 按名称排序
        ///         date            : 按最后更新时间排序
        ///     
        ///     默认查询条件：最后更新时间倒序
        /// 
        /// </remarks>
        /// <param name="queryInfo">查询信息。</param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.RoleController.SearchPage")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [Route("Search")]
        [HttpPost]
        public PagedResult<RoleView> SearchPage([FromBody]RoleQuery queryInfo)
        {
            return _Service.FilterPage(queryInfo);
        }

        /// <summary>
        /// 为下拉列表提供数据 - 获取所有的角色列表 
        /// </summary>
        /// <response code="200">
        /// 操作成功                   
        /// 返回的结果中SelectView对象无code属性
        /// </response>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.RoleController.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [Route("Select")]
        [HttpGet]
        public List<SelectView> Select()
        {
            RoleQuery queryInfo = new RoleQuery();
            return _Service.Select(queryInfo);
        }
        #endregion

        #region 初始化角色
        /// <summary>
        /// 初始化角色   清除已有角色，重置管理员角色信息
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.RoleController.Init")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("InitAdmin")]
        [HttpPost]
        public void Init()
        {
            _Service.InitAdmin();
        }
        #endregion
    }
}

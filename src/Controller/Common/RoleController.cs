using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
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
        /// 新增
        /// </summary>
        /// <param name="view">请求体中放置新增角色的信息，新增时无需传递ID值</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpPost("")]
        public ResultView Create([FromBody]RoleView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改   
        /// </summary>
        /// <param name="view">请求体中带入修改角色的信息，修改时请指定ID值</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpPut("")]
        public ResultView Update([FromBody]RoleView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        #endregion

        #region 数据删除
        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="id">要删除的角色id</param>
        /// <power>删除</power>       
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpDelete("Delete/{id}")]
        public ResultView Delete(string id)
        {
            return _Service.Delete(id, LogonInfo);
        }
        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <param name="id">要删除的角色id</param>
        /// <power>粉碎数据</power>       
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Remove")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpDelete("Remove/{id}")]
        public ResultView Remove(string id)
        {
            return _Service.Remove(id, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 根据ID获取一个角色信息      
        /// </summary>
        /// <param name="id">要获取的对象ID</param>
        /// <power>详情</power>       
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Single")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpGet("{id}")]
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
        ///         name         : 按名称排列
        ///         date         : 按最后更新时间排列   为默认排序
        ///     
        /// </remarks>
        /// <param name="queryInfo">查询信息。（包含分页信息、查询条件、排序条件）</param>
        /// <returns></returns>
        /// <power>查询</power>       
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpPost("Search")]
        public PagedResult<RoleView> SearchPage([FromBody]RoleQuery queryInfo)
        {
            return _Service.FilterPage(queryInfo);
        }

        /// <summary>
        /// 获取所有的角色列表 - 主要为下拉列表提供数据
        /// </summary>
        /// <response code="200">
        /// 操作成功。    返回的结果中SelectView对象code属性均为空
        /// </response>
        /// <power>列表选择</power>       
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpGet("Select")]
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
        /// <power>重置管理员角色</power>
        /// <remarks>
        /// 重置管理员角色信息，重置信息包含菜单、功能点
        /// 注：只要系统中有叫“管理员”字样的角色均被重置。如果不存在则添加名为“系统管理员”的角色。
        /// </remarks>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Role.Init")]
        [SwaggerOperation(Tags = new[] { "系统管理-角色管理" })]
        [HttpPost("InitAdmin")]
        public void Init()
        {
            _Service.InitAdmin();
        }
        #endregion
    }
}

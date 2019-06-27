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
    /// 菜单管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Menu")]
    public class MenuController : DataController
    {
        #region 构造方法
        private readonly MenuService _Service;
        private readonly ILogger<MenuController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public MenuController(MenuService service, ILogger<MenuController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 查询单页面的菜单信息，以树形结构显示，无分页信息
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Tree")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpGet("")]
        public List<MenuMainView> Tree()
        {
            return _Service.SearchMainTree();
        }
        ///// <summary>
        ///// 查询所有的菜单信息，以树形结构显示，无分页信息
        ///// </summary>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Tree")]
        //[SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        //[Route("All")]
        //[HttpGet]
        //public List<MenuMainView> ALL()
        //{
        //    return _Service.ManageMultipleTree();
        //}

        /// <summary>
        /// 查询单页面的菜单信息，以树形结构显示，无分页信息
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Tree")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpGet("Single")]
        public List<MenuMainView> ManageSingleTree()
        {
            return _Service.SearchMainTree(MenuType.ManageSingle);
        }

        /// <summary>
        /// 查询多页面菜单信息，以树形结构显示，无分页信息
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Tree")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpGet("Multiple")]
        public List<MenuMainView> ManageMultipleTree()
        {
            return _Service.SearchMainTree(MenuType.ManageMultiple);
        }
        #endregion

        /// <summary>
        /// 初始化菜单   清除已有菜单，重新设置默认菜单
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Init")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpPost("Init")]
        public void Init()
        {
            _Service.Init();
        }

        #region 新增修改数据
        /// <summary>
        /// 新增一个主菜单
        /// </summary>
        /// <param name="view">请求体中放置新增对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpPost("")]
        public ResultView Create([FromBody]MenuMainView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改一个主菜单
        /// </summary>
        /// <param name="view">请求体中带入修改对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpPut("")]
        public ResultView Update([FromBody]MenuMainView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        #endregion

        #region 数据删除
        /// <summary>
        /// 删除一个主菜单
        /// </summary>
        /// <param name="id">要删除的对象id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Menu.Remove")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [HttpDelete("Remove/{id}")]
        public ResultView Remove(string id)
        {
            return _Service.Remove(id, LogonInfo);
        }
        #endregion
    }
}


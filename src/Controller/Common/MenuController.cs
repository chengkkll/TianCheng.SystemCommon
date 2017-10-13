using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
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
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.MenuController.Tree")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("")]
        [HttpGet]
        public List<MenuMainView> Tree()
        {
            return _Service.AllTree();
        }
        /// <summary>
        /// 查询所有的菜单信息，以树形结构显示，无分页信息
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.MenuController.Tree")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("All")]
        [HttpGet]
        public List<MenuMainView> ALL()
        {
            return _Service.ManageMultipleTree();
        }

        /// <summary>
        /// 查询单页面的菜单信息，以树形结构显示，无分页信息
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.MenuController.Tree")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("Single")]
        [HttpGet]
        public List<MenuMainView> ManageSingleTree()
        {
            return _Service.ManageSingleTree();
        }

        /// <summary>
        /// 查询多页面菜单信息，以树形结构显示，无分页信息
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.MenuController.Tree")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("Multiple")]
        [HttpGet]
        public List<MenuMainView> ManageMultipleTree()
        {
            return _Service.ManageMultipleTree();
        }
        #endregion

        /// <summary>
        /// 初始化菜单   清除已有菜单，重新设置默认菜单
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.MenuController.Init")]
        [SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        [Route("Init")]
        [HttpPost]
        public void Init()
        {
            _Service.Init();
        }
        ///// <summary>
        ///// 追加菜单
        ///// </summary>
        //[SwaggerOperation(Tags = new[] { "系统管理-权限管理" })]
        //[Route("Append")]
        //[HttpPost]
        //public void Append()
        //{
        //    _Service.AppendMenu();
        //}
    }
}


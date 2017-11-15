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
    /// 组织机构管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : DataController
    {
        #region 构造方法
        private readonly DepartmentService _Service;
        private readonly ILogger<DepartmentController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public DepartmentController(DepartmentService service, ILogger<DepartmentController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        #region 新增修改数据
        /// <summary>
        /// 新增一个组织机构
        /// </summary>
        /// <param name="view">请求体中放置新增对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("")]
        [HttpPost]
        public ResultView Create([FromBody]DepartmentView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改一个组织机构
        /// </summary>
        /// <param name="view">请求体中带入修改对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("")]
        [HttpPut]
        public ResultView Update([FromBody]DepartmentView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        #endregion

        #region 数据删除
        /// <summary>
        /// 删除一个组织机构
        /// </summary>
        /// <param name="id">要删除的对象id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("{id}")]
        [HttpDelete]
        public ResultView Delete(string id)
        {
            return _Service.Delete(id, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 根据ID获取一个组织机构信息            
        /// </summary>
        /// <param name="id">组织机构ID</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.SearchById")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("{id}")]
        [HttpGet]
        public DepartmentView SearchById(string id)
        {
            return _Service.SearchById(id);
        }

        /// <summary>
        /// 查询组织结构列表（分页 + 查询条件）
        /// </summary>
        /// <remarks> 
        ///     排序规则包含： 
        /// 
        ///         nameAsc         : 按名称正序排列
        ///         nameDesc        : 按名称倒序排列          
        ///         dateAsc         : 按最后更新时间正序排列
        ///         dateDesc        : 按最后更新时间倒序排列   为默认排序
        ///         
        /// </remarks> 
        /// <param name="queryInfo">查询信息。（包含分页信息、查询条件、排序条件）
        /// 排序规则包含： 【nameAsc：按名称正序排列 】、【nameDesc：按名称倒序排列】、【dateAsc：按最后更新时间正序排列】、【dateDesc：按最后更新时间倒序排列，默认排序】
        /// </param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.SearchPage")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("Search")]
        [HttpPost]
        public PagedResult<DepartmentView> SearchPage([FromBody]DepartmentQuery queryInfo)
        {
            return _Service.FilterPage(queryInfo);
        }

        /// <summary>
        /// 查询组织结构列表（无分页 + 查询条件）
        /// </summary>
        /// <remarks> 
        ///     排序规则包含： 
        /// 
        ///         nameAsc         : 按名称正序排列
        ///         nameDesc        : 按名称倒序排列          
        ///         dateAsc         : 按最后更新时间正序排列
        ///         dateDesc        : 按最后更新时间倒序排列   
        ///         indexAsc        : 按序号正序排列   为默认排序
        ///         indexDesc       : 按序号倒序排列   
        ///         
        /// </remarks> 
        /// <param name="queryInfo">查询信息。（包含查询条件、排序条件）
        /// 排序规则包含： 【nameAsc：按名称正序排列 】、【nameDesc：按名称倒序排列】、【dateAsc：按最后更新时间正序排列】、【dateDesc：按最后更新时间倒序排列，默认排序】
        /// </param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.SearchFilter")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("SearchALL")]
        [HttpPost]
        public List<DepartmentView> SearchFilter([FromBody]DepartmentQuery queryInfo)
        {
            return _Service.Filter(queryInfo);
        }

        /// <summary>
        /// 为下拉列表提供数据 - 获取所有的组织机构列表
        /// </summary>        
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("Select")]
        [HttpGet]
        public List<SelectView> Select()
        {
            DepartmentQuery query = new DepartmentQuery();
            return _Service.Select(query);
        }

        /// <summary>
        /// 查询指定机构下的子机构
        /// </summary>
        /// <param name="id">机构管理id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.Sub")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("{id}/Sub")]
        [HttpGet]
        public List<SelectView> Sub(string id)
        {
            DepartmentQuery query = new DepartmentQuery() { ParentId = id };
            return _Service.Select(query);
        }

        /// <summary>
        /// 查询当前用户的所有下级部门 下拉列表显示
        /// </summary>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.DepartmentController.Sub")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [Route("My/Sub")]
        [HttpGet]
        public List<SelectView> MySub()
        {
            if (String.IsNullOrWhiteSpace(LogonInfo.DepartmentId))
            {
                ApiException.ThrowBadRequest("您需要先有所属部门才可执行此操作");
            }

            DepartmentQuery query = new DepartmentQuery() { ParentId = LogonInfo.DepartmentId };
            List<SelectView> subList = _Service.Select(query);
            subList.Insert(0, new SelectView() { Id = LogonInfo.DepartmentId, Name = LogonInfo.DepartmentName });
            return subList;
        }
        #endregion

    }
}

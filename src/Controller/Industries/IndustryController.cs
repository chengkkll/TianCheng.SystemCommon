using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;
using System.Linq;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 行业管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Industry")]
    public class IndustryController : DataController
    {
        #region 构造方法
        private readonly IndustryService _Service;
        private readonly ILogger<IndustryController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public IndustryController(IndustryService service, ILogger<IndustryController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        #region 新增修改数据
        /// <summary>
        /// 新增行业
        /// </summary>
        /// <param name="view">请求体中放置新增对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Industry.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("")]
        [HttpPost]
        public ResultView Create([FromBody]IndustryView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改行业
        /// </summary>
        /// <param name="view">请求体中带入修改对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Industry.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("")]
        [HttpPut]
        public ResultView Update([FromBody]IndustryView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        #endregion

        #region 数据删除
        /// <summary>
        /// 删除行业
        /// </summary>
        /// <param name="id">要删除的对象id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Industry.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("{id}")]
        [HttpDelete]
        public ResultView Delete(string id)
        {
            return _Service.Delete(id, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 查看行业详情            
        /// </summary>
        /// <param name="id">组织机构ID</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Industry.SearchById")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("{id}")]
        [HttpGet]
        public IndustryView SearchById(string id)
        {
            return _Service.SearchById(id);
        }

        /// <summary>
        /// 查询行业分页列表
        /// </summary>
        /// <remarks> 
        ///     排序规则包含： 
        /// 
        ///         name        : 按名称排列
        ///         date        : 按最后更新时间排列   为默认排序
        ///         
        /// </remarks> 
        /// <param name="queryInfo"></param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Industry.SearchPage")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("Search")]
        [HttpPost]
        public PagedResult<IndustryView> SearchPage([FromBody]IndustryQuery queryInfo)
        {
            return _Service.FilterPage(queryInfo);
        }

        /// <summary>
        /// 获取所有行业
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [HttpGet("SearchAll")]
        public List<IndustryView> SearchAll()
        {
            var list = _Service._Filter(new IndustryQuery() { Sort = new QuerySort() { IsAsc = true, Property = "name" } });
            return AutoMapper.Mapper.Map<List<IndustryView>>(list.ToList());
        }

        /// <summary>
        /// 获取可用行业列表
        /// </summary>        
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Industry.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [HttpGet("Select")]
        public List<SelectView> Select()
        {
            IndustryQuery query = new IndustryQuery();
            return _Service.Select(query);
        }
        #endregion
    }
}

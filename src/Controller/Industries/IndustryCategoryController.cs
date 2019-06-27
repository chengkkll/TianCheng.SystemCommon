using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 行业管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Industry/Category")]
    public class IndustryCategoryController : DataController
    {
        #region 构造方法
        private readonly IndustryCategoryService _Service;
        private readonly ILogger<IndustryCategoryController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public IndustryCategoryController(IndustryCategoryService service, ILogger<IndustryCategoryController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        #region 新增修改数据
        /// <summary>
        /// 新增行业分类
        /// </summary>
        /// <param name="view">请求体中放置新增对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.IndustryCategory.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("")]
        [HttpPost]
        public ResultView Create([FromBody]IndustryCategoryView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改行业分类
        /// </summary>
        /// <param name="view">请求体中带入修改对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.IndustryCategory.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("")]
        [HttpPut]
        public ResultView Update([FromBody]IndustryCategoryView view)
        {
            return _Service.Update(view, LogonInfo);
        }
        #endregion

        #region 数据删除
        /// <summary>
        /// 删除行业分类
        /// </summary>
        /// <param name="id">要删除的对象id</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.IndustryCategory.Delete")]
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
        /// 查看行业分类详情            
        /// </summary>
        /// <param name="id">组织机构ID</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.IndustryCategory.SearchById")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("{id}")]
        [HttpGet]
        public IndustryCategoryView SearchById(string id)
        {
            return _Service.SearchById(id);
        }

        /// <summary>
        /// 查询行业分类分页列表
        /// </summary>
        /// <remarks> 
        ///     排序规则包含： 
        /// 
        ///         name        : 按名称排列
        ///         date        : 按最后更新时间排列   为默认排序
        ///         
        /// </remarks> 
        /// <param name="queryInfo"></param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.IndustryCategory.SearchPage")]
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("Search")]
        [HttpPost]
        public PagedResult<IndustryCategoryView> SearchPage([FromBody]IndustryCategoryQuery queryInfo)
        {
            return _Service.FilterPage(queryInfo);
        }

        /// <summary>
        /// 获取行业的下拉列表数据（按分类排列）
        /// </summary>        
        [SwaggerOperation(Tags = new[] { "系统管理-行业管理" })]
        [Route("Select")]
        [HttpGet]
        public List<IndustryCategoryView> Select()
        {
            var list = _Service._Filter(new IndustryCategoryQuery());
            return AutoMapper.Mapper.Map<List<IndustryCategoryView>>(list.ToList());
        }
        #endregion
    }
}

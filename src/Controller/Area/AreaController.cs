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
    /// 区域管理接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/Area")]
    public class AreaController : DataController
    {
        #region 构造方法
        private readonly AreaService _Service;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        public AreaController(AreaService service)
        {
            _Service = service;
        }
        #endregion

        #region 新增修改数据
        /// <summary>
        /// 初始化区域信息
        /// </summary>
        [SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        [HttpPost("InitArea")]
        public void InitArea()
        {
            _Service.DefaultInit();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="view"></param>
        [SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        [HttpPost("")]
        public ResultView Create([FromBody]AreaView view)
        {
            return _Service.Create(view, LogonInfo);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="view"></param>
        [SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        [HttpPut("")]
        public ResultView Update([FromBody]AreaView view)
        {
            return _Service.Update(view, LogonInfo);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据ID获取组织机构详情            
        /// </summary>
        /// <power>详情</power>
        /// <param name="id">组织机构ID</param>
        [SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        [HttpGet("{id}")]
        public AreaView SearchById(string id)
        {
            return _Service.SearchById(id);
        }
        /// <summary>
        /// 查询组织结构列表（分页 + 查询条件）
        /// </summary>
        /// <remarks> 
        ///     排序规则包含： 
        /// 
        ///         name         : 按名称排列
        ///         code         : 按编码排列
        ///         date         : 按最后更新时间排列   为默认排序
        ///         
        /// </remarks>
        /// <param name="queryInfo">查询信息。（包含分页信息、查询条件、排序条件）</param>
        /// <power>查询</power>
        [SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        [HttpPost("Search")]
        public PagedResult<AreaView> SearchPage([FromBody]AreaQuery queryInfo)
        {
            return _Service.FilterPage(queryInfo);
        }

        /// <summary>
        /// 获取所有省份列表
        /// </summary>
        [SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        [HttpGet("Province")]
        public List<SelectView> Province()
        {
            return _Service.Select(new AreaQuery { AreaType = AreaType.Province });
        }

        /// <summary>
        /// 获取指定省份下的城市列表
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        [HttpGet("{provinceId}/City")]
        public List<SelectView> City(string provinceId)
        {
            return _Service.Select(new AreaQuery { AreaType = AreaType.City, SuperiorId = provinceId });
        }
        #endregion
    }
}

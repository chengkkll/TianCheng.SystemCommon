using Microsoft.AspNetCore.Mvc;
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
    /// 组织机构管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : DataController
    {
        #region 构造方法
        private readonly DepartmentService _Service;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        public DepartmentController(DepartmentService service)
        {
            _Service = service;
        }
        #endregion

        #region 新增修改数据
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="view">请求体中放置新增部门的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Create")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpPost("")]
        public ResultView Create([FromBody]DepartmentView view)
        {
            return _Service.Create(view, LogonInfo);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="view">请求体中带入修改部门的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpPut("")]
        public ResultView Update([FromBody]DepartmentView view)
        {
            return _Service.Update(view, LogonInfo);
        }

        #endregion

        #region 数据删除
        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="id">要删除的部门id</param>
        /// <power>删除</power>       
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Delete")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpDelete("Delete/{id}")]
        public ResultView Delete(string id)
        {
            return _Service.Delete(id, LogonInfo);
        }
        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <param name="id">要删除的部门id</param>
        /// <power>粉碎数据</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Remove")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpDelete("Remove/{id}")]
        public ResultView Remove(string id)
        {
            return _Service.Remove(id, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 根据ID获取组织机构详情            
        /// </summary>
        /// <power>详情</power>
        /// <param name="id">组织机构ID</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Single")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpGet("{id}")]
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
        ///         name         : 按名称排列
        ///         code         : 按编码排列
        ///         parent       : 按上级部门名称排列
        ///         index        : 按部门序号排列
        ///         date         : 按最后更新时间排列   为默认排序
        ///         
        /// </remarks> 
        /// <param name="queryInfo">查询信息。（包含分页信息、查询条件、排序条件）</param>
        /// <power>查询</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpPost("Search")]
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
        ///         name         : 按名称排列
        ///         code         : 按编码排列
        ///         parent       : 按上级部门名称排列
        ///         index        : 按部门序号排列
        ///         date         : 按最后更新时间排列   为默认排序
        ///         
        /// </remarks> 
        /// <param name="queryInfo">查询信息。（包含查询条件、排序条件）</param>
        /// <power>查询</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpPost("SearchALL")]
        public List<DepartmentView> SearchFilter([FromBody]DepartmentQuery queryInfo)
        {
            return _Service.Filter(queryInfo);
        }

        /// <summary>
        /// 获取所有的组织机构列表
        /// </summary>
        /// <remarks> 返回的下级对象结构为SelectView，适合用作下拉列表中的显示</remarks>
        /// <power>列表选择</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpGet("Select")]
        public List<SelectView> Select()
        {
            DepartmentQuery query = new DepartmentQuery();
            return _Service.Select(query);
        }
        /// <summary>
        /// 获取根部门
        /// </summary>
        /// <power>列表选择</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpGet("Root")]
        public DepartmentView Root()
        {
            var info = _Service.SearchQueryable().Where(e => string.IsNullOrEmpty(e.ParentId)).FirstOrDefault();
            return AutoMapper.Mapper.Map<DepartmentView>(info);
        }

        ///// <summary>
        ///// 获取根部门
        ///// </summary>
        ///// <returns></returns>
        ///// <power>列表选择</power>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Select")]
        //[SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        //[HttpGet("Root/Multiple")]
        //public List<SelectView> GetMultipleRoot()
        //{
        //    return AutoMapper.Mapper.Map<List<SelectView>>(_Service.GetRoot());
        //}

        /// <summary>
        /// 查询指定机构下的子机构
        /// </summary>
        /// <param name="id">机构管理id</param>
        /// <remarks> 返回的下级对象结构为SelectView，适合用作下拉列表中的显示</remarks>
        /// <power>列表选择</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpGet("{id}/Sub")]
        public List<SelectView> Sub(string id)
        {
            DepartmentQuery query = new DepartmentQuery() { ParentId = id };
            return _Service.Select(query);
        }

        /// <summary>
        /// 查询当前用户的所有下级部门
        /// </summary>
        /// <remarks> 返回的下级对象结构为SelectView，适合用作下拉列表中的显示</remarks>
        /// <power>列表选择</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Department.Select")]
        [SwaggerOperation(Tags = new[] { "系统管理-组织机构管理" })]
        [HttpGet("My/Sub")]
        public List<SelectView> MySub()
        {
            if (string.IsNullOrWhiteSpace(LogonInfo.DepartmentId))
            {
                ApiException.ThrowBadRequest("您需要先有所属部门才可执行此操作");
            }

            DepartmentQuery query = new DepartmentQuery() { ParentId = LogonInfo.DepartmentId };
            List<SelectView> subList = _Service.Select(query);
            return subList;
        }
        #endregion

    }
}

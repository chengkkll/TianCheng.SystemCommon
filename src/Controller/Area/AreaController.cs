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
using TianCheng.BaseService.PlugIn.LoadApi;
using Newtonsoft.Json;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 区域管理接口
    /// </summary>
    //[Produces("application/json")]
    //[Route("api/Area")]
    public class AreaController : DataController
    {
        #region 构造方法
        private readonly AreaService _Service;
        private readonly ILogger<AreaController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public AreaController(AreaService service, ILogger<AreaController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion

        //#region 新增修改数据
        ///// <summary>
        ///// 新增一个区域信息
        ///// </summary>
        ///// <param name="view">请求体中放置新增区域的信息</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.Create")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("")]
        //[HttpPost]
        //public ResultView Create([FromBody]AreaView view)
        //{
        //    return _Service.Create(view, LogonInfo);
        //}

        ///// <summary>
        ///// 修改一个区域信息
        ///// </summary>
        ///// <param name="view">请求体中带入修改区域的信息</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.Update")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("")]
        //[HttpPut]
        //public ResultView Update([FromBody]AreaView view)
        //{
        //    return _Service.Update(view, LogonInfo);
        //}
        //#endregion

        ///// <summary>
        ///// 初始化区域信息
        ///// </summary>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.Init")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("init")]
        //[HttpPost]
        //public ResultView Init()
        //{
        //    _Service.DefaultInit();
        //    return ResultView.Success();
        //}

        //#region 数据删除
        ///// <summary>
        ///// 删除区域信息
        ///// </summary>
        ///// <param name="id">要删除的区域信息id</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.Delete")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("{id}")]
        //[HttpDelete]
        //public ResultView Delete(string id)
        //{
        //    //逻辑删除
        //    return _Service.Delete(id, LogonInfo);
        //}
        //#endregion



        //#region 数据查询
        ///// <summary>
        ///// 根据ID获取一条区域信息            
        ///// </summary>
        ///// <param name="id">查询的区域信息ID</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.SearchById")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("{id}")]
        //[HttpGet]
        //public AreaView SearchById(string id)
        //{
        //    return _Service.SearchById(id);
        //}

        ///// <summary>
        ///// 根据条件获取有分页信息的查询列表
        ///// </summary>
        ///// <remarks> 
        /////     排序规则包含： 
        ///// 
        /////         nameAsc         : 按名称正序排列
        /////         nameDesc        : 按名称倒序排列        
        /////         codeAsc         : 按编码正序排列
        /////         codeDesc        : 按编码倒序排列          
        /////         dateAsc         : 按最后更新时间正序排列
        /////         dateDesc        : 按最后更新时间倒序排列   为默认排序
        /////         
        ///// </remarks> 
        ///// <param name="queryInfo">查询信息。（包含分页信息、查询条件、排序条件）
        ///// </param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.SearchPage")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("Search")]
        //[HttpPost]
        //public PagedResult<AreaView> SearchPage([FromBody]AreaQuery queryInfo)
        //{
        //    return _Service.FilterPage(queryInfo);
        //}

        ///// <summary>
        ///// 根据条件查询数据列表  无分页效果 
        ///// </summary>
        ///// <remarks> 
        /////     排序规则包含： 
        ///// 
        /////         nameAsc         : 按名称正序排列
        /////         nameDesc        : 按名称倒序排列        
        /////         codeAsc         : 按编码正序排列
        /////         codeDesc        : 按编码倒序排列          
        /////         dateAsc         : 按最后更新时间正序排列
        /////         dateDesc        : 按最后更新时间倒序排列   为默认排序
        /////         
        ///// </remarks> 
        ///// <param name="queryInfo">查询信息。（包含查询条件、排序条件）
        ///// </param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.SearchFilter")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("SearchALL")]
        //[HttpPost]
        //public List<AreaView> SearchFilter([FromBody]AreaQuery queryInfo)
        //{
        //    return _Service.Filter(queryInfo);
        //}

        ///// <summary>
        ///// 为下拉列表提供数据 - 获取所有的区域列表
        ///// </summary>        
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.Select")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("Select")]
        //[HttpGet]
        //public List<SelectView> Select()
        //{
        //    AreaQuery query = new AreaQuery();
        //    return _Service.Select(query);
        //}

        ///// <summary>
        ///// 查询指定区域下的子区域
        ///// </summary>
        ///// <param name="code">区域编码</param>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.Sub")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("{code}/Sub")]
        //[HttpGet]
        //public List<SelectView> Sub(string code)
        //{
        //    AreaQuery query = new AreaQuery() { SuperiorCode = code };
        //    return _Service.Select(query);
        //}

        ///// <summary>
        ///// 获取所有的省份信息（包括直辖市）
        ///// </summary>
        ///// <returns></returns>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.Province")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("Province")]
        //[HttpGet]
        //public List<AreaView> Province()
        //{
        //    AreaQuery query = new AreaQuery() { AreaType = AreaType.Province | AreaType.Municipality };
        //    return _Service.Filter(query);
        //}

        ///// <summary>
        ///// 获取某省份下的所有的城市信息
        ///// </summary>
        ///// <param name="province_name">省份名称</param>
        ///// <returns></returns>
        //[Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.AreaController.ProvinceCity")]
        //[SwaggerOperation(Tags = new[] { "系统管理-区域管理" })]
        //[Route("{province_name}/City")]
        //[HttpGet]
        //public List<AreaView> ProvinceCity(string province_name)
        //{
        //    AreaQuery query = new AreaQuery() { AreaType = AreaType.City, SuperiorName = province_name };
        //    return _Service.Filter(query);
        //}
        //#endregion

        private string LoadArea(string area)
        {
            string url;
            if (String.IsNullOrWhiteSpace(area))
            {
                url = "http://restapi.amap.com/v3/config/district?key=26783229f3c043e9281864044f1154e0";
            }
            url = "http://restapi.amap.com/v3/config/district?key=26783229f3c043e9281864044f1154e0&keywords=" + area;

            return LoadApi.GetJson(url);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HttpGet("InitProvince")]
        public string InitProvince()
        {
            string json = LoadArea("");

            Amap map = Newtonsoft.Json.JsonConvert.DeserializeObject<Amap>(json);

            if (map.Status == 0)
            {
                return json;
            }
            List<DistrictsInfo> disList = map.Districts;
            if (map.Districts != null && map.Districts.Count > 0 && map.Districts[0].Level == "country")
            {
                disList = map.Districts[0].Districts;
            }

            List<AreaInfo> areaList = new List<AreaInfo>();
            foreach (var item in disList)
                areaList.Add(new AreaInfo()
                {
                    Name = item.Name,
                    Code = item.CityCode.ToString(),
                    TelephoneCode = item.CityCode.ToString(),
                    AreaType = GetAreaType(item),
                    ShortName = GetShortName(item)
                });
            foreach (var area in areaList)
            {
                _Service.Create(area, LogonInfo);
            }
            //json = Newtonsoft.Json.JsonConvert.SerializeObject(areaList);


            return json;
        }

        private string GetShortName(DistrictsInfo dist)
        {
            string name = dist.Name;
            return name.Replace("省", "").Replace("市", "").Replace("特别行政区", "")
                       .Replace("壮族自治区", "").Replace("回族自治区", "").Replace("维吾尔自治区", "").Replace("自治区", "");
        }

        private AreaType GetAreaType(DistrictsInfo dist)
        {
            if (dist.Name.Contains("北京") || dist.Name.Contains("天津") ||
                dist.Name.Contains("重庆") || dist.Name.Contains("上海") ||
                dist.Name.Contains("香港") || dist.Name.Contains("澳门"))
            {
                return AreaType.Municipality;
            }

            switch (dist.Level)
            {
                case "country": return AreaType.Country;
                case "province": return AreaType.Province;
                case "city": return AreaType.City;
                default: return AreaType.None;
            }
        }
    }
}

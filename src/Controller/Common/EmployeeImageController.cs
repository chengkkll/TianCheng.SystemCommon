using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.BaseService.PlugIn.Swagger;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 员工管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeImageController : UploadImageController
    {
        #region 构造方法
        private readonly EmployeeService _Service;
        private readonly ILogger<EmployeeImageController> _logger;

        /// <summary> 
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>        
        public EmployeeImageController(EmployeeService service, ILogger<EmployeeImageController> logger)
        {
            _Service = service;
            _logger = logger;
        }
        #endregion



        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="employee_id">请求体中带入修改对象的信息</param>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.SetHeadImg")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("{employee_id}/SetHeadImg")]
        [HttpPatch]
        [SwaggerFileUpload]
        public ResultView SetHeadImg(string employee_id)
        {
            string webFile = $"~/wwwroot/UploadFiles/Employee/Head/{employee_id}/{Guid.NewGuid().ToString()}";
            UploadFileInfo file = SaveSingleFile(webFile);

            var info = _Service.SearchById(employee_id);
            info.HeadImg = file.WebFileName;
            _Service.Update(info, LogonInfo);
            return ResultView.Success("头像修改成功");
        }

        /// <summary>
        /// 按属性修改用户信息
        /// </summary>
        /// <remarks> 
        /// 
        ///     property包含（不区分大小写）： 
        /// 
        ///         Position      : 职位          
        ///         Name          : 名称 
        ///         Nickname      : 昵称
        ///         Mobile        : 手机电话
        ///         Telephone     : 座机电话
        ///         
        /// </remarks>
        /// <param name="view"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.EmployeeController.Update")]
        [SwaggerOperation(Tags = new[] { "系统管理-员工管理" })]
        [Route("SetProperty")]
        [HttpPatch]
        public ResultView SetProperty([FromBody] SetPropertyView view)
        {
            if (String.IsNullOrWhiteSpace(view.PropertyName))
            {
                ApiException.ThrowBadRequest("属性名不能为空");
            }

            var info = _Service._SearchById(LogonInfo.Id);
            switch (view.PropertyName.ToLower())
            {
                case "position": { info.Position = view.PropertyValue; break; }
                case "name": { info.Name = view.PropertyValue; break; }
                case "nickname": { info.Nickname = view.PropertyValue; break; }
                case "mobile": { info.Mobile = view.PropertyValue; break; }
                case "telephone": { info.Telephone = view.PropertyValue; break; }
            }
            _Service.Update(info, LogonInfo);
            return ResultView.Success("信息修改成功");
        }
    }
}

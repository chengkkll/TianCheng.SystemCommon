using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace SamplesWebApi.Service
{
    /// <summary>
    /// 部门服务的扩展处理
    /// </summary>
    public class DepartmentServiceOptionExt : IServiceExtOption
    {
        #region 构造方法
        /// <summary>
        /// 获取服务接口
        /// </summary>
        static private IServiceCollection _services;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="services"></param>
        public DepartmentServiceOptionExt(IServiceCollection services)
        {
            _services = services;
        }
        #endregion

        /// <summary>
        /// 设置扩展
        /// </summary>
        public void SetOption()
        {
            // 设置部门保存时的验证处理
            //DepartmentServiceOption.Option.SavingCheck = OnSavingCheck;

            //// 设置部门更新的后置处理
            //DepartmentServiceOption.Option.Updated = OnUpdated;

        }

        /// <summary>
        /// 部门信息的保存验证处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="logonInfo"></param>
        public void OnSavingCheck(DepartmentInfo entity, TokenLogonInfo logonInfo)
        {
            // 如果验证不通过，可以通过抛出异常的形式终止保存操作，系统会将异常信息按Json形式返回到调用端。
            // ApiException.ThrowBadRequest("部门信息自定义异常信息");
        }

        /// <summary>
        /// 保存后置处理
        /// </summary>
        /// <param name="newEntity"></param>
        /// <param name="oldEntity"></param>
        /// <param name="logonInfo"></param>
        public void OnUpdated(DepartmentInfo newEntity, DepartmentInfo oldEntity, TokenLogonInfo logonInfo)
        {
            IServiceProvider _ServiceProvider = _services.BuildServiceProvider();
            // 部门的主管修改后，需要修改企业的申请
            // CompanyApplyService companyApplyService = (CompanyApplyService)_ServiceProvider.GetService(typeof(CompanyApplyService));
            // if (companyApplyService != null)
            // {
            //     companyApplyService.OnDepartmentChange(newEntity, oldEntity, logonInfo);
            // }
        }
    }
}

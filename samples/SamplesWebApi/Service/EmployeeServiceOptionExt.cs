using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianCheng.BaseService;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace SamplesWebApi.Service
{
    /// <summary>
    /// 员工服务的扩展处理
    /// </summary>
    public class EmployeeServiceOptionExt : IServiceExtOption
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
        public EmployeeServiceOptionExt(IServiceCollection services)
        {
            _services = services;
        }
        #endregion

        /// <summary>
        /// 设置扩展
        /// </summary>
        public void SetOption()
        {
            //设置角色是否必填
            EmployeeServiceOption.Option.RequiredRole = true;
            //设置部门信息是否必填
            EmployeeServiceOption.Option.RequiredDepartment = true;
            // 排列方式为执行顺序的方式
            // 设置员工保存时的验证处理
            EmployeeServiceOption.Option.SavingCheck = OnSavingCheck;
            // 设置员工保存的前置处理
            EmployeeServiceOption.Option.Saving = OnSaving;
            // 设置员工更新的后置处理
            EmployeeServiceOption.Option.Updated = OnUpdated;
            // 设置员工保存的后置处理
            EmployeeServiceOption.Option.Saved = OnSaved;
        }

        #region 事件处理
        /// <summary>
        /// 员工保存时的验证处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="logonInfo"></param>
        public void OnSavingCheck(EmployeeInfo entity, TokenLogonInfo logonInfo)
        {
            // 如果验证不通过，可以通过抛出异常的形式终止保存操作，系统会将异常信息按Json形式返回到调用端。
            // ApiException.ThrowBadRequest("员工信息自定义异常信息");
        }
        /// <summary>
        /// 员工保存的前置处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="logonInfo"></param>
        public void OnSaving(EmployeeInfo entity, TokenLogonInfo logonInfo)
        {

        }

        /// <summary>
        /// 员工更新的后置处理
        /// </summary>
        /// <param name="newEentity"></param>
        /// <param name="oldEntity"></param>
        /// <param name="logonInfo"></param>
        public void OnUpdated(EmployeeInfo newEentity, EmployeeInfo oldEntity, TokenLogonInfo logonInfo)
        {

        }
        /// <summary>
        /// 员工保存的后置处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="logonInfo"></param>
        public void OnSaved(EmployeeInfo entity, TokenLogonInfo logonInfo)
        {

        }
        #endregion
    }
}

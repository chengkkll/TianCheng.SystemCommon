using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TianCheng.BaseService;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    static public class TianChengCommonServicesExt
    {
        /// <summary>
        /// 增加业务的Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void TianChengCommonInit(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.TianChengInit(configuration);
            //读取配置信息
            services.Configure<FunctionModuleConfig>(configuration.GetSection("FunctionModule"));
        }
    }
}

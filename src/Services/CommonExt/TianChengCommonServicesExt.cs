using Microsoft.Extensions.Configuration;
using TianCheng.SystemCommon.Model;

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
        public static void TianChengCommonInit(this IServiceCollection services, IConfiguration configuration)
        {
            services.TianChengBaseServicesInit(configuration);

            // 注册功能模块配置信息
            services.Configure<FunctionModuleConfig>(configuration.GetSection("FunctionModule"));
        }
    }
}

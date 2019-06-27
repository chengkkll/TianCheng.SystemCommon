using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 自动注册服务
    /// </summary>
    static public class TianChengBaseConfigureServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void TianChengCommonInit(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.TianChengBaseServicesInit(configuration);
        }
    }
}

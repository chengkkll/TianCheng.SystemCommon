using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService.PlugIn;

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
            app.TianChengInit(configuration);
        }
    }
}

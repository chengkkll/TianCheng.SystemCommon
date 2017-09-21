using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace SamplesWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddMvc();

            //string xmlPath = $"{AppContext.BaseDirectory}\\LibraryComments";
            //if (!System.IO.Directory.Exists(xmlPath))
            //{
            //    System.IO.Directory.CreateDirectory(xmlPath);
            //}

            //foreach (CompilationLibrary library in DependencyContext.Default.CompileLibraries)
            //{
            //    if (library.Name.Contains("TianCheng"))
            //    {


            //        Assembly assembly = Assembly.Load(new AssemblyName(library.Name));
            //        if (assembly != null && !String.IsNullOrWhiteSpace(assembly.Location))
            //        {
            //            string path = System.IO.Path.GetDirectoryName(assembly.Location);
            //            foreach (string file in System.IO.Directory.GetFiles(path, "*.xml"))
            //            {
            //                //将xml文件拷贝到运行目录
            //                string desc = $"{xmlPath}\\{System.IO.Path.GetFileName(file)}";
            //                System.IO.File.Copy(file, desc);
            //            }
            //        }
            //    }
            //}

            services.TianChengCommonInit(Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            TianCheng.SystemCommon.Services.AuthService authService)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //app.UseMvc();

            app.TianChengCommonInit(Configuration, loggerFactory, authService);
        }
    }
}

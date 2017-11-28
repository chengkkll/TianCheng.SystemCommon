﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using TianCheng.BaseService;
using TianCheng.SystemCommon.Model;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.DAL;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 功能点 [ Service ]
    /// </summary>
    public class FunctionService : BusinessService<FunctionModuleInfo, FunctionModuleView, FunctionQuery>
    {
        #region 构造方法
        /// <summary>
        /// 模块配置信息
        /// </summary>
        FunctionModuleConfig ModuleConfig;
        IHostingEnvironment _host;
        ILogger<FunctionService> _Logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="servicesProvider"></param>
        /// <param name="configuration"></param>
        /// <param name="host"></param>
        public FunctionService(FunctionDAL dal, ILogger<FunctionService> logger, IServiceProvider servicesProvider,
            IConfiguration configuration,
            IHostingEnvironment host)
            : base(dal, logger, servicesProvider)
        {
            var node = configuration.GetSection("FunctionModule:ModuleDict");

            ModuleConfig = new FunctionModuleConfig() { ModuleDict = new Dictionary<string, string>() };

            for (int i = 0; true; i++)
            {
                string code = configuration.GetSection($"FunctionModule:ModuleDict:{i}:Code").Value;
                if (String.IsNullOrWhiteSpace(code))
                {
                    break;
                }
                string name = configuration.GetSection($"FunctionModule:ModuleDict:{i}:Name").Value;
                ModuleConfig.ModuleDict.Add(code, name);
            }



            //if (moduleConfig.Value != null)
            //{
            //    ModuleConfig = moduleConfig.Value;
            //}
            _host = host;
            _Logger = logger;
        }
        #endregion

        #region 数据初始化
        /// <summary>
        /// 初始化功能点列表
        /// 读取运行目录所有程序集中拥有[Microsoft.AspNetCore.Authorization.Authorize(Policy = "xxx")]特性的方法，
        /// 并将Policy作为功能点Code，将方法注释作为功能点Name
        /// </summary>
        public void Init()
        {
            //初始化功能模块
            Dictionary<string, FunctionModuleInfo> moduleDict = new Dictionary<string, FunctionModuleInfo>();
            int moduleIndex = 1;
            foreach (string key in ModuleConfig.ModuleDict.Keys)
            {
                if (moduleDict.ContainsKey(key))
                {
                    continue;
                }
                FunctionModuleInfo module = new FunctionModuleInfo()
                {
                    Index = moduleIndex++,
                    Name = ModuleConfig.ModuleDict[key],
                    Code = key
                };
                if (!String.IsNullOrEmpty(module.Code) && !moduleDict.ContainsKey(module.Code))
                {
                    moduleDict.Add(module.Code, module);
                }
            }

            //初始化功能分组
            Dictionary<string, FunctionGroupInfo> groupDict = new Dictionary<string, FunctionGroupInfo>();
            foreach (var assembly in TianCheng.Model.AssemblyHelper.GetAssemblyList())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (groupDict.ContainsKey(type.Name))
                    {
                        continue;
                    }
                    FunctionGroupInfo group = new FunctionGroupInfo()
                    {
                        Code = type.Name,
                        Name = GetSummary($"T:{type.FullName}"),
                        ModeuleCode = type.FullName.Replace("." + type.Name, "")
                    };
                    if (String.IsNullOrWhiteSpace(group.Name))
                    {
                        group.Name = type.FullName;
                    }
                    if (!String.IsNullOrEmpty(group.Name) && !groupDict.ContainsKey(group.Name))
                    {
                        groupDict.Add(group.Name, group);
                    }
                }
            }
            //初始化功能点
            List<FunctionInfo> funList = new List<FunctionInfo>();
            foreach (var assembly in TianCheng.Model.AssemblyHelper.GetAssemblyList())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var method in type.GetMethods())
                    {
                        var attribute = method.GetCustomAttribute<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>(false);
                        if (attribute == null)
                        {
                            continue;
                        }
                        FunctionInfo fun = new FunctionInfo()
                        {
                            Policy = attribute.Policy,
                            Code = method.Name,
                            GroupCode = GetSummary($"T:{type.FullName}"),
                            ModeuleCode = type.FullName.Replace("." + type.Name, ""),
                            Name = GetSummary($"M:{ type.FullName}.{ method.Name}")
                        };
                        if (String.IsNullOrWhiteSpace(fun.GroupCode))
                        {
                            fun.GroupCode = type.FullName;
                        }
                        funList.Add(fun);
                    }
                }
            }


            //设置分组下的功能点
            foreach (FunctionInfo fun in funList)
            {
                if (groupDict.ContainsKey(fun.GroupCode))
                {
                    groupDict[fun.GroupCode].FunctionList.Add(fun);
                }
            }
            //设置模块下的分组
            foreach (FunctionGroupInfo group in groupDict.Values)
            {
                if (group.FunctionList.Count > 0)
                {
                    foreach (string code in moduleDict.Keys)
                    {
                        if (group.ModeuleCode.Contains(code))
                        {
                            moduleDict[code].FunctionGroups.Add(group);
                        }
                    }
                }
            }
            List<FunctionModuleInfo> moduleList = moduleDict.Values.ToList();
            for (int i = 0; i < moduleList.Count; i++)
            {
                if (moduleList[i].FunctionGroups.Count == 0)
                {
                    moduleList.RemoveAt(i);
                    i--;
                }
            }
            //保存数据
            _Dal.Drop();
            _Dal.Insert(moduleList);
        }

        #region 注释文档操作
        /// <summary>
        /// 注释文档列表
        /// </summary>
        private List<XDocument> docList = null;
        /// <summary>
        /// 注释文档列表
        /// </summary>
        private List<XDocument> DocList
        {
            get
            {
                if (docList == null)
                {
                    docList = new List<XDocument>();

                    //获取注释文件所在路径
                    var basePath = _host.ContentRootPath;
                    //var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                    foreach (string file in System.IO.Directory.GetFiles(basePath, "*.xml"))
                    {
                        docList.Add(XDocument.Load(file));
                    }

                    foreach (Microsoft.Extensions.DependencyModel.CompilationLibrary library in Microsoft.Extensions.DependencyModel.DependencyContext.Default.CompileLibraries)
                    {
                        if (!library.Name.Contains("TianCheng"))
                        {
                            if (library.Serviceable) continue;
                            if (library.Type == "package") continue;
                        }

                        try
                        {
                            Assembly assembly = Assembly.Load(new AssemblyName(library.Name));
                            if (assembly != null)
                            {
                                string assemblyPath = System.IO.Path.GetDirectoryName(assembly.Location);
                                if (!String.IsNullOrWhiteSpace(assemblyPath))
                                {
                                    foreach (string file in System.IO.Directory.GetFiles(assemblyPath, "*.xml"))
                                    {
                                        docList.Add(XDocument.Load(file));
                                    }
                                }

                            }
                        }
                        catch
                        {
                            //程序集无法反射时跳过
                        }
                    }
                }
                return docList;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private string GetSummary(string method)
        {
            foreach (var doc in DocList)
                foreach (var ele in doc.Root.Element("members").Elements("member"))
                {
                    //获取control信息
                    var member = ele.Attribute("name").Value.ToString();
                    if (member.Contains(method))
                    {
                        if (ele.Element("power") != null)
                        {
                            return ele.Element("power").Value?.ToString().Replace("\n", "").Replace("\r", "").Trim();
                        }
                        return ele.Element("summary")?.Value?.ToString().Replace("\n", "").Replace("\r", "").Trim();
                    }
                }
            return String.Empty;
        }
        #endregion
        #endregion

        #region 查询
        /// <summary>
        /// 查询所有的功能点列表，按树形显示
        /// </summary>
        /// <returns></returns>
        public List<FunctionModuleView> LoadTree()
        {
            var list = _Dal.Queryable().ToList();
            return AutoMapper.Mapper.Map<List<FunctionModuleView>>(list);
        }
        /// <summary>
        /// 获取所有的功能点列表
        /// </summary>
        /// <returns></returns>
        public List<FunctionView> SearchFunction()
        {
            List<FunctionView> list = new List<FunctionView>();
            var query = _Dal.Queryable().ToList();
            foreach (var module in query)
            {
                foreach (var group in module.FunctionGroups)
                {
                    foreach (var fun in group.FunctionList)
                    {
                        list.Add(AutoMapper.Mapper.Map<FunctionView>(fun));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取所有的功能点列表
        /// </summary>
        /// <returns></returns>
        static public List<FunctionView> FunctionList()
        {
            List<FunctionView> list = new List<FunctionView>();
            FunctionDAL _Dal = new FunctionDAL();
            var query = _Dal.Queryable().ToList();
            foreach (var module in query)
            {
                foreach (var group in module.FunctionGroups)
                {
                    foreach (var fun in group.FunctionList)
                    {
                        list.Add(AutoMapper.Mapper.Map<FunctionView>(fun));
                    }
                }
            }
            return list;
        }
        #endregion
    }
}

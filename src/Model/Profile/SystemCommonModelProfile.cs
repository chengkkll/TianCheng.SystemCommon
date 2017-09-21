using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 系统基础信息的AutoMapper转换
    /// </summary>
    public class SystemCommonModelProfile : Profile, IAutoProfile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SystemCommonModelProfile()
        {
            Register();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Register()
        {
            CreateMap<EmployeeInfo, EmployeeView>();
            CreateMap<EmployeeView, EmployeeInfo>();
            CreateMap<EmployeeInfo, SelectView>();

            CreateMap<FunctionModuleInfo, FunctionModuleView>();
            CreateMap<FunctionGroupInfo, FunctionGroupView>();
            CreateMap<FunctionInfo, FunctionView>();
            CreateMap<FunctionModuleView, FunctionModuleInfo>();
            CreateMap<FunctionGroupView, FunctionGroupInfo>();
            CreateMap<FunctionView, FunctionInfo>();

            CreateMap<MenuMainInfo, MenuMainView>();
            CreateMap<MenuMainView, MenuMainInfo>();
            CreateMap<MenuSubInfo, MenuSubView>();
            CreateMap<MenuSubView, MenuSubInfo>();

            CreateMap<RoleInfo, RoleView>();
            CreateMap<RoleView, RoleInfo>();
            CreateMap<RoleInfo, SelectView>();
            CreateMap<RoleInfo, RoleSimpleView>();

            CreateMap<DepartmentInfo, DepartmentView>();
            CreateMap<DepartmentView, DepartmentInfo>();
            CreateMap<DepartmentInfo, SelectView>();

            CreateMap<AreaView, AreaInfo>();
            CreateMap<AreaInfo, AreaView>();
            CreateMap<AreaInfo, SelectView>();
            
        }
    }
}

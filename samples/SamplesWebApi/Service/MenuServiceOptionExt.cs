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
    /// 菜单服务的扩展处理
    /// </summary>
    public class MenuServiceOptionExt : IServiceExtOption
    {
        #region 构造方法
        /// <summary>
        /// 获取服务接口
        /// </summary>
        private IServiceCollection _Service;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        public MenuServiceOptionExt(IServiceCollection service)
        {
            _Service = service;
        }
        #endregion

        /// <summary>
        /// 设置扩展
        /// </summary>
        public void SetOption()
        {
            //初始化菜单数据
            MenuServiceOption.Option.InitMenuData = InitMenuData;
        }

        /// <summary>
        /// 初始化菜单数据
        /// </summary>
        /// <param name="mainList">已设置的菜单信息</param>
        public void InitMenuData(List<MenuMainInfo> mainList)
        {


            MenuMainInfo mainSystem = new MenuMainInfo() { Name = "系统管理", Link = "System", Icon = "el-icon-setting", Type = MenuType.ManageSingle, Index = 6 };
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "部门管理", Link = "System.Departments", Type = MenuType.ManageSingle, Index = 1 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "菜单管理", Link = "System.Menus", Type = MenuType.ManageSingle, Index = 3 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "员工管理", Link = "System.Employees", Type = MenuType.ManageSingle, Index = 5 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "角色管理", Link = "System.Roles", Type = MenuType.ManageSingle, Index = 7 });

            MenuMainInfo mainPersonal = new MenuMainInfo() { Name = "个人中心", Link = "PersonalCenter", Icon = "el-icon-setting", Type = MenuType.ManageSingle, Index = 5 };
            mainPersonal.SubMenu.Add(new MenuSubInfo() { Name = "基本信息", Link = "PersonalCenter.Common", Type = MenuType.ManageSingle, Index = 2 });
            mainPersonal.SubMenu.Add(new MenuSubInfo() { Name = "修改密码", Link = "PersonalCenter.ChangePwd", Type = MenuType.ManageSingle, Index = 4 });

            mainList.Clear();
            mainList.Add(mainSystem);
            mainList.Add(mainPersonal);
        }
    }
}

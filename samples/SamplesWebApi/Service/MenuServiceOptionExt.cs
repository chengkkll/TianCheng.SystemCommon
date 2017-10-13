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
            MenuMainInfo main1 = new MenuMainInfo() { Name = "基础数据", Index = 1 };
            main1.SubMenu.Add(new MenuSubInfo() { Name = "拒绝原因", Sref = "app.basic.disagreed", Index = 1 });
            main1.SubMenu.Add(new MenuSubInfo() { Name = "客户信息", Sref = "app.basic.client", Index = 1 });

            MenuMainInfo main2 = new MenuMainInfo() { Name = "录音管理", Index = 2 };
            main2.SubMenu.Add(new MenuSubInfo() { Name = "接单管理", Sref = "app.recording.takers", Index = 1 });
            main2.SubMenu.Add(new MenuSubInfo() { Name = "我的录音", Sref = "app.recording.edit", Index = 2 });
            main2.SubMenu.Add(new MenuSubInfo() { Name = "审核录音", Sref = "app.recording.check", Index = 3 });

            MenuMainInfo mainSystem = new MenuMainInfo() { Name = "系统管理", Index = 6 };
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "部门管理", Sref = "app.system.department", Index = 1 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "员工管理", Sref = "app.system.auth_management", Index = 3 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "角色管理", Sref = "app.system.roles", Index = 5 });

            MenuMainInfo mainPersonal = new MenuMainInfo() { Name = "个人信息", Index = 5 };
            mainPersonal.SubMenu.Add(new MenuSubInfo() { Name = "修改密码", Sref = "app.personal.change_my_login_password", Index = 4 });

            mainList.Clear();
            mainList.Add(main1);
            mainList.Add(main2);
            mainList.Add(mainSystem);
            mainList.Add(mainPersonal);
        }
    }
}

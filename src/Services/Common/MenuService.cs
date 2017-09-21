using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianCheng.BaseService;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    ///  菜单      [ Service ]
    /// </summary>
    public class MenuService : BusinessService<MenuMainInfo, MenuMainView, MenuQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        public MenuService(MenuMainDAL dal, ILogger<MenuService> logger)
            : base(dal, logger)
        {

        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询多页面的菜单列表，按树形显示
        /// </summary>
        /// <returns></returns>
        public List<MenuMainView> ManageMultipleTree()
        {
            var list = _Dal.SearchQueryable().Where(e => e.Type == MenuType.ManageMultiple).OrderBy(e=>e.Index).ToList();
            return AutoMapper.Mapper.Map<List<MenuMainView>>(list);
        }
        /// <summary>
        /// 查询多页面的菜单列表，按树形显示
        /// </summary>
        /// <returns></returns>
        public List<MenuMainView> ManageSingleTree()
        {
            var list = _Dal.SearchQueryable().Where(e => e.Type == MenuType.ManageSingle).OrderBy(e => e.Index).ToList();
            return AutoMapper.Mapper.Map<List<MenuMainView>>(list);
        }

        /// <summary>
        /// 查询所有的菜单列表，按树形显示
        /// </summary>
        /// <returns></returns>
        public List<MenuMainView> AllTree()
        {
            var list = _Dal.SearchQueryable().OrderBy(e => e.Index).ToList();
            return AutoMapper.Mapper.Map<List<MenuMainView>>(list);
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void Init()
        {
            List<MenuMainInfo> mainList = new List<MenuMainInfo>();
            MenuMainInfo main1 = new MenuMainInfo() { Name = "商会管理", Index = 1, FontAwesome = "gear", Type = MenuType.ManageMultiple };
            main1.SubMenu.Add(new MenuSubInfo() { Name = "商会信息", Sref = "coc_info.html", Index = 1, Type = MenuType.ManageMultiple });
            main1.SubMenu.Add(new MenuSubInfo() { Name = "商会动态", Sref = "coc_noti.html", Index = 2, Type = MenuType.ManageMultiple });
            main1.SubMenu.Add(new MenuSubInfo() { Name = "组织机构", Sref = "coc_org.html", Index = 3, Type = MenuType.ManageMultiple });
            main1.SubMenu.Add(new MenuSubInfo() { Name = "首页图片", Sref = "coc_home_img.html", Index = 4, Type = MenuType.ManageMultiple });
            mainList.Add(main1);

            MenuMainInfo main2 = new MenuMainInfo() { Name = "会员管理", Index = 2, FontAwesome = "users", Type = MenuType.ManageMultiple };
            main2.SubMenu.Add(new MenuSubInfo() { Name = "会员管理", Sref = "user_manage.html", Index = 1, Type = MenuType.ManageMultiple });
            main2.SubMenu.Add(new MenuSubInfo() { Name = "会员动态", Sref = "user_noti.html", Index = 2, Type = MenuType.ManageMultiple });

            mainList.Add(main2);

            MenuMainInfo main3 = new MenuMainInfo() { Name = "财务管理", Index = 3, FontAwesome = "cny", Type = MenuType.ManageMultiple };
            main3.SubMenu.Add(new MenuSubInfo() { Name = "财务明细", Sref = "finance_manage.html", Index = 1, Type = MenuType.ManageMultiple });
            mainList.Add(main3);

            MenuMainInfo main4 = new MenuMainInfo() { Name = "系统管理", Index = 4, FontAwesome = "info", Type = MenuType.ManageMultiple };
            main4.SubMenu.Add(new MenuSubInfo() { Name = "行业信息", Sref = "system_industry.html", Index = 1, Type = MenuType.ManageMultiple });
            main4.SubMenu.Add(new MenuSubInfo() { Name = "收支项目", Sref = "system_fina_item.html", Index = 3, Type = MenuType.ManageMultiple });
            main4.SubMenu.Add(new MenuSubInfo() { Name = "密码修改", Sref = "system_change_pwd.html", Index = 4, Type = MenuType.ManageMultiple });
            mainList.Add(main4);

            MenuMainInfo sing1 = new MenuMainInfo() { Name = "系统管理", Index = 12, Type = MenuType.ManageSingle };
            sing1.SubMenu.Add(new MenuSubInfo() { Name = "组织机构", Sref = "app.system.department", Index = 1, Type = MenuType.ManageSingle });
            sing1.SubMenu.Add(new MenuSubInfo() { Name = "员工管理", Sref = "app.system.auth_management", Index = 3, Type = MenuType.ManageSingle });
            sing1.SubMenu.Add(new MenuSubInfo() { Name = "角色管理", Sref = "app.system.roles", Index = 5, Type = MenuType.ManageSingle });
            sing1.SubMenu.Add(new MenuSubInfo() { Name = "密码修改", Sref = "app.system.change_my_pwd", Index = 6, Type = MenuType.ManageSingle });
            mainList.Add(sing1);

            MenuMainInfo sing2 = new MenuMainInfo() { Name = "商会管理", Index = 11, Type = MenuType.ManageSingle };
            sing2.SubMenu.Add(new MenuSubInfo() { Name = "商会信息", Sref = "app.basic.coc_info", Index = 1, Type = MenuType.ManageSingle });
            sing2.SubMenu.Add(new MenuSubInfo() { Name = "行业信息", Sref = "app.basic.industry", Index = 3, Type = MenuType.ManageSingle });
            sing2.SubMenu.Add(new MenuSubInfo() { Name = "首页图片", Sref = "app.basic.home_image", Index = 5, Type = MenuType.ManageSingle });
            sing2.SubMenu.Add(new MenuSubInfo() { Name = "会员管理", Sref = "app.member.company", Index = 1, Type = MenuType.ManageSingle });
            sing2.SubMenu.Add(new MenuSubInfo() { Name = "动态管理", Sref = "app.noti.noti_management", Index = 2, Type = MenuType.ManageSingle });
            mainList.Add(sing2);


            if (MenuServiceOption.Option.InitMenuData != null)
            {
                MenuServiceOption.Option.InitMenuData(mainList);
            }
            _Dal.Drop();
            _Dal.Save(mainList);
        }
        #endregion

        //public void AppendMenu()
        //{
        //    //var spalist = _Dal.SearchQueryable().Where(e => e.Type == MenuType.ManageSingle).ToList();
        //    //_Dal.Remove(spalist);

        //    List<MenuMainInfo> mainList = new List<MenuMainInfo>();

        //    MenuMainInfo main3 = new MenuMainInfo() { Name = "财务管理", Sref = "app.finance", Index = 12, Type = MenuType.ManageSingle };
        //    main3.SubMenu.Add(new MenuSubInfo() { Name = "财务图片", Sref = "app.finance.images", Index = 1, Type = MenuType.ManageSingle });
        //    mainList.Add(main3);

        //    _Dal.Save(mainList);

        //}
    }
}

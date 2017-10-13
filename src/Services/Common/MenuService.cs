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
            MenuMainInfo mainSystem = new MenuMainInfo() { Name = "系统管理", Index = 20 };
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "部门管理", Sref = "app.system.department", Index = 1 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "员工管理", Sref = "app.system.employee", Index = 3 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "角色管理", Sref = "app.system.roles", Index = 5 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "修改密码", Sref = "app.personal.change_my_login_password", Index = 4 });

            mainList.Add(mainSystem);

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

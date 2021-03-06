﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    ///  菜单      [ Service ]
    /// </summary>
    public class MenuService : MongoBusinessService<MenuMainInfo, MenuMainView, MenuQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="servicesProvider"></param>
        public MenuService(MenuMainDAL dal, ILogger<MenuService> logger, IServiceProvider servicesProvider)
            : base(dal)
        {

        }

        #endregion

        private MenuType DefaultMenuType = MenuType.ManageSingle;

        #region 查询
        /// <summary>
        /// 获取主菜单的树形结构
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public List<MenuMainView> SearchMainTree(MenuType menuType = MenuType.None)
        {
            var query = _Dal.Queryable();
            switch (menuType)
            {
                case MenuType.None: { break; }
                case MenuType.ManageMultiple:
                case MenuType.ManageSingle: { query = query.Where(e => e.Type == menuType); break; }
                default: { query = query.Where(e => e.Type == MenuType.ManageSingle); break; }
            }
            var list = query.OrderBy(e => e.Index).ToList();
            return AutoMapper.Mapper.Map<List<MenuMainView>>(list);
        }
        ///// <summary>
        ///// 查询多页面的菜单列表，按树形显示
        ///// </summary>
        ///// <returns></returns>
        //public List<MenuMainView> ManageMultipleTree()
        //{
        //    var list = _Dal.Queryable().Where(e => e.Type == MenuType.ManageMultiple).OrderBy(e => e.Index).ToList();
        //    return AutoMapper.Mapper.Map<List<MenuMainView>>(list);
        //}
        ///// <summary>
        ///// 查询多页面的菜单列表，按树形显示
        ///// </summary>
        ///// <returns></returns>
        //public List<MenuMainView> ManageSingleTree()
        //{
        //    var list = _Dal.Queryable().Where(e => e.Type == MenuType.ManageSingle).OrderBy(e => e.Index).ToList();
        //    return AutoMapper.Mapper.Map<List<MenuMainView>>(list);
        //}
        ///// <summary>
        ///// 查询所有的菜单列表，按树形显示
        ///// </summary>
        ///// <returns></returns>
        //public List<MenuMainView> AllTree()
        //{
        //    var list = _Dal.Queryable().OrderBy(e => e.Index).ToList();
        //    return AutoMapper.Mapper.Map<List<MenuMainView>>(list);
        //}
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void Init()
        {
            List<MenuMainInfo> mainList = new List<MenuMainInfo>();
            MenuMainInfo mainSystem = new MenuMainInfo() { Name = "系统管理", Index = 20 };
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "部门管理", Link = "app.system.department", Index = 1 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "员工管理", Link = "app.system.employee", Index = 3 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "角色管理", Link = "app.system.roles", Index = 5 });
            mainSystem.SubMenu.Add(new MenuSubInfo() { Name = "修改密码", Link = "app.personal.change_my_login_password", Index = 4 });
            mainList.Add(mainSystem);

            MenuServiceOption.Option.InitMenuData?.Invoke(mainList);

            _Dal.Drop();
            foreach (var main in mainList)
            {
                Create(main, new TokenLogonInfo() { });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void Saving(MenuMainInfo info, TokenLogonInfo logonInfo)
        {
            if (info.Type == MenuType.None)
            {
                info.Type = DefaultMenuType;
            }
        }
        #endregion

        /// <summary>
        /// 保存主菜单的前置验证
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void SavingCheck(MenuMainInfo info, TokenLogonInfo logonInfo)
        {
            if (string.IsNullOrWhiteSpace(info.Name))
            {
                ApiException.ThrowBadRequest("主菜单名称不能为空");
            }
            if (info.Index <= 0)
            {
                info.Index = 10;
            }
            if (info.Type == MenuType.None)
            {
                info.Type = DefaultMenuType;
            }

            foreach (var sub in info.SubMenu)
            {
                if (string.IsNullOrWhiteSpace(sub.Name))
                {
                    ApiException.ThrowBadRequest("子菜单名称不能为空");
                }
                if (string.IsNullOrWhiteSpace(sub.Link))
                {
                    ApiException.ThrowBadRequest("子菜单的地址不能为空");
                }
                if (sub.Index <= 0)
                {
                    sub.Index = 10;
                }
                if (sub.Type == MenuType.None)
                {
                    sub.Type = DefaultMenuType;
                }
            }
        }


        /// <summary>
        /// 保存一个子菜单信息   如果对于的父菜单不存在会新建
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="link"></param>
        /// <param name="parentName"></param>
        public void SaveSubMenu(string name, int index, string link, string parentName)
        {
            SaveSubMenu(new MenuSubInfo() { Name = name, Index = index, Link = link }, parentName);
        }
        /// <summary>
        /// 保存一个子菜单信息   如果对于的父菜单不存在会新建
        /// </summary>
        /// <param name="subMenu"></param>
        /// <param name="parentName"></param>
        public void SaveSubMenu(MenuSubInfo subMenu, string parentName)
        {
            if (string.IsNullOrEmpty(subMenu.Link))
            {
                TianCheng.Model.ApiException.ThrowBadRequest("菜单地址不能为空");
            }

            var main = GetMainByName(parentName);
            if (main == null)
            {
                main = CreateMainMenu(parentName);
            }

            var sub = main.SubMenu.Where(e => e.Link == subMenu.Link).FirstOrDefault();
            if (sub == null)
            {
                main.SubMenu.Add(subMenu);
            }
            else
            {
                sub.Name = subMenu.Name;
                sub.Index = subMenu.Index;
                sub.Link = subMenu.Link;
            }
            Update(main, new TokenLogonInfo() { });
        }

        /// <summary>
        /// 在父菜单中删除一个子菜单
        /// </summary>
        /// <param name="subName"></param>
        /// <param name="parentName"></param>
        public void RemoveSubMenu(string subName, string parentName)
        {
            if (string.IsNullOrEmpty(subName) || string.IsNullOrEmpty(parentName))
            {
                TianCheng.Model.ApiException.ThrowBadRequest("菜单名称不能为空");
            }

            var main = GetMainByName(parentName);
            if (main == null)
            {
                return;
            }

            for (int i = 0; i < main.SubMenu.Count; i++)
            {
                if (main.SubMenu[i].Name == subName)
                {
                    main.SubMenu.RemoveAt(i);
                    break;
                }
            }

            Update(main, new TokenLogonInfo() { });
        }

        /// <summary>
        /// 根据菜单名新增一个主菜单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private MenuMainInfo CreateMainMenu(string name)
        {
            MenuMainInfo main = new MenuMainInfo()
            {
                Name = name,
                Index = 10,
                Type = DefaultMenuType,
                Link = string.Empty,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            Create(main, new TokenLogonInfo() { });
            return main;
        }
        /// <summary>
        /// 保存一个主菜单信息 （子菜单不变）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="link"></param>
        public void SaveMainMenu(string name, int index, string link)
        {
            SaveMainMenu(new MenuMainInfo() { Name = name, Index = index, Link = link });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public void SaveMainMenu(string name, int index)
        {
            SaveMainMenu(new MenuMainInfo() { Name = name, Index = index });
        }

        /// <summary>
        /// 保存一个主菜单信息 （子菜单不变）
        /// </summary>
        /// <param name="mainInfo"></param>
        public void SaveMainMenu(MenuMainInfo mainInfo)
        {
            if (string.IsNullOrEmpty(mainInfo.Name))
            {
                ApiException.ThrowBadRequest("菜单名称不能为空");
            }

            var main = GetMainByName(mainInfo.Name);
            if (main != null)
            {
                main.Index = main.Index;
                main.Link = main.Link;
                _Dal.UpdateObject(main);
            }
            else
            {
                _Dal.InsertObject(main);
            }
        }
        /// <summary>
        /// 根据主菜单名称获取一个主菜单信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private MenuMainInfo GetMainByName(string name)
        {
            return _Dal.Queryable().Where(e => e.Name == name).FirstOrDefault();
        }
    }
}

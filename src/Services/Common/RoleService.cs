using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianCheng.BaseService;
using TianCheng.DAL.MongoDB;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 角色管理    [ Service ]
    /// </summary>
    public class RoleService : BusinessService<RoleInfo, RoleView, RoleQuery>
    {
        #region 构造方法
        MenuService _MenuService;
        FunctionService _FunctionService;
        EmployeeService _EmployeeService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="menuService"></param>
        /// <param name="functionService"></param>
        /// <param name="employeeService"></param>
        public RoleService(RoleDAL dal, ILogger<RoleService> logger,
           MenuService menuService, FunctionService functionService, EmployeeService employeeService)
            : base(dal, logger)
        {
            _MenuService = menuService;
            _FunctionService = functionService;
            _EmployeeService = employeeService;
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<RoleInfo> _Filter(RoleQuery input)
        {
            var query = _Dal.SearchQueryable();

            #region 查询条件
            //不显示删除的数据
            query = query.Where(e => e.IsDelete == false);

            // 按名称的模糊查询
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Name));
            }

            #endregion

            #region 设置排序规则
            //设置排序方式
            switch (input.OrderBy)
            {
                case "nameAsc": { query = query.OrderBy(e => e.Name); break; }
                case "nameDesc": { query = query.OrderByDescending(e => e.Name); break; }
                case "dateAsc": { query = query.OrderBy(e => e.UpdateDate); break; }
                case "dateDesc": { query = query.OrderByDescending(e => e.UpdateDate); break; }
                default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            }
            #endregion

            //返回查询结果
            return query;
        }

        /// <summary>
        /// 获取一个以角色名称为key的字典信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, RoleInfo> GetNameDict()
        {
            Dictionary<string, RoleInfo> dict = new Dictionary<string, RoleInfo>();
            foreach (RoleInfo info in _Dal.SearchQueryable().Where(e => e.IsDelete == false))
            {
                if (!dict.ContainsKey(info.Name))
                {
                    dict.Add(info.Name, info);
                }
            }
            return dict;
        }
        #endregion

        #region 新增 / 修改方法
        /// <summary>
        /// 更新的后置操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="old"></param>
        /// <param name="logonInfo"></param>
        protected override void Updated(RoleInfo info, RoleInfo old, TokenLogonInfo logonInfo)
        {
            //如果角色名称发生改变，修改对应的用户信息中的角色
            if (!info.Name.Equals(old.Name))
            {
                _EmployeeService.OnRoleChanged(info);
            }
        }
        #endregion

        #region 删除方法

        /// <summary>
        /// 删除时的检查
        /// </summary>
        /// <param name="info"></param>
        protected override void DeleteRemoveCheck(RoleInfo info)
        {
            //如果角色下有员工信息，不允许删除
            if (_EmployeeService.CountByRoleId(info.Id.ToString()) > 0)
            {
                throw ApiException.BadRequest("角色下有员工信息，不允许删除");
            }
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 初始化管理员角色信息
        /// </summary>
        public void InitAdmin()
        {
            //删除已有的角色信息
            List<RoleInfo> roleList = _Dal.SearchQueryable().Where(e => !String.IsNullOrEmpty(e.Name) && e.Name.Contains("管理员")).ToList();
            if (roleList == null || roleList.Count == 0)
            {
                roleList = new List<RoleInfo>();
                roleList.Add(new RoleInfo() { Name = "系统管理员", Desc = "系统默认系统管理员" });
            }
            //添加管理员角色
            foreach (RoleInfo admin in roleList)
            {
                admin.PagePower = _MenuService.ManageMultipleTree();
                admin.FunctionPower = _FunctionService.SearchFunction();
                admin.CreateDate = DateTime.Now;
                admin.CreaterId = "";
                admin.CreaterName = "系统初始化";
                _Dal.Save(admin);
            }
        }
        /// <summary>
        /// 更新管理员的角色信息
        /// </summary>
        public void UpdateAdminRole()
        {
            RoleInfo admin = _Dal.SearchQueryable().Where(e => e.Name == "系统管理员").FirstOrDefault();
            if (admin == null)
            {
                admin = new RoleInfo() { Name = "系统管理员", Desc = "系统默认系统管理员" };

                admin.CreateDate = DateTime.Now;
                admin.CreaterId = "";
                admin.CreaterName = "系统初始化";
                admin.ProcessState = ProcessState.Edit;

            }
            admin.PagePower = _MenuService.ManageMultipleTree();
            admin.FunctionPower = _FunctionService.SearchFunction();
            admin.UpdateDate = DateTime.Now;
            admin.UpdaterId = "";
            admin.UpdaterName = "系统初始化";
            admin.IsDelete = false;

            //保存管理员角色信息
            _Dal.Save(admin);
        }
        #endregion

    }
}

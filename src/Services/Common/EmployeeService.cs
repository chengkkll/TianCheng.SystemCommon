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
    /// 员工管理    [ Service ]
    /// </summary>
    public class EmployeeService : BusinessService<EmployeeInfo, EmployeeView, EmployeeQuery>
    {
        #region 构造方法
        private DepartmentDAL _DepDal;
        private RoleDAL _RoleDal;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="depDal"></param>
        /// <param name="roleDal"></param>
        public EmployeeService(EmployeeDAL dal, ILogger<EmployeeService> logger,
            DepartmentDAL depDal, RoleDAL roleDal) : base(dal, logger)
        {
            _RoleDal = roleDal;
            _DepDal = depDal;
        }
        #endregion

        #region 查询方法

        /// <summary>
        /// 根据id查询对象信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override EmployeeView SearchById(string id)
        {
            //根据ID获取员工信息
            var info = _SearchById(id);
            //返回
            return AutoMapper.Mapper.Map<EmployeeView>(info);
        }

        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<EmployeeInfo> _Filter(EmployeeQuery input)
        {
            //_logger.LogInformation("this is a log test: input.code:{0}, input.key:{1}", input.Name, input.State);
            var query = _Dal.Queryable();

            #region 查询条件
            // 逻辑删除的过滤 0-不显示逻辑删除的数据 1-显示所有数据，包含逻辑删除的   2-只显示逻辑删除的数据
            if (input.HasDelete == 0)
            {
                query = query.Where(e => e.IsDelete == false);
            }
            if (input.HasDelete == 2)
            {
                query = query.Where(e => e.IsDelete);
            }

            // 按名称的模糊查询
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Name));
            }
            // 按部门ID查询
            if (!string.IsNullOrWhiteSpace(input.DepartmentId))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.Department.Id) && e.Department.Id == input.DepartmentId);
            }
            //根据根节点查询部门
            if (!string.IsNullOrWhiteSpace(input.RootDepartment))
            {
                List<string> depIds = GetSubDepartment(input.RootDepartment);
                depIds.Add(input.RootDepartment);
                query = query.Where(e => e.Department != null && !String.IsNullOrEmpty(e.Department.Id) && depIds.Contains(e.Department.Id));
            }
            // 按角色ID查询
            if (!string.IsNullOrWhiteSpace(input.RoleId))
            {
                query = query.Where(e => e.Role != null && e.Role.Id == input.RoleId);
            }

            //按状态查询  1-可用，3-锁住，5-禁用
            switch (input.State)
            {
                case UserState.Enable: { query = query.Where(e => e.State == UserState.Enable); break; }
                case UserState.LogonLock: { query = query.Where(e => e.State == UserState.LogonLock); break; }
                case UserState.Disable: { query = query.Where(e => e.State == UserState.Disable); break; }
            }
            #endregion

            #region 设置排序规则
            //设置排序方式
            //switch (input.OrderBy)
            //{
            //    case "nameAsc": { query = query.OrderBy(e => e.Name); break; }
            //    case "nameDesc": { query = query.OrderByDescending(e => e.Name); break; }
            //    case "depNameAsc": { query = query.OrderBy(e => e.Department.Name); break; }
            //    case "depNameDesc": { query = query.OrderByDescending(e => e.Department.Name); break; }
            //    case "roleNameAsc": { query = query.OrderBy(e => e.Role.Name); break; }
            //    case "roleNameDesc": { query = query.OrderByDescending(e => e.Role.Name); break; }
            //    case "stateAsc": { query = query.OrderBy(e => e.State); break; }
            //    case "stateDesc": { query = query.OrderByDescending(e => e.State); break; }
            //    case "dateAsc": { query = query.OrderBy(e => e.UpdateDate); break; }
            //    case "dateDesc": { query = query.OrderByDescending(e => e.UpdateDate); break; }
            //    default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            //}
            switch (input.Sort.Property)
            {
                case "name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name); break; }
                case "depName": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Department.Name) : query.OrderByDescending(e => e.Department.Name); break; }
                case "roleName": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Role.Name) : query.OrderByDescending(e => e.Role.Name); break; }
                case "state": { query = input.Sort.IsAsc ? query.OrderBy(e => e.State) : query.OrderByDescending(e => e.State); break; }
                case "date": { query = input.Sort.IsAsc ? query.OrderBy(e => e.UpdateDate) : query.OrderByDescending(e => e.UpdateDate); break; }
                default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            }


            #endregion

            //返回查询结果
            return query;
        }
        /// <summary>
        /// 获取所有的子部门ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal List<string> GetSubDepartment(string id)
        {
            List<string> subIds = new List<string>();
            var list = _DepDal.Queryable().Where(e => e.ParentId == id).ToList();
            foreach (var item in list)
            {
                string itemId = item.Id.ToString();
                subIds.Add(itemId);
                subIds.AddRange(GetSubDepartment(itemId));
            }
            return subIds;
        }
        #endregion

        #region 新增 / 修改方法
        /// <summary>
        /// 保存的校验
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void SavingCheck(EmployeeInfo info, TokenLogonInfo logonInfo)
        {
            //处理角色的验证
            if (EmployeeServiceOption.Option.RequiredRole)
            {
                if (info.Role == null)
                {
                    throw ApiException.BadRequest("请指定用户的角色");
                }
                if (info.Role != null && String.IsNullOrWhiteSpace(info.Role.Id))
                {
                    var role = _RoleDal.SearchById(info.Role.Id);
                    if (role == null)
                    {
                        throw ApiException.BadRequest("无法找到指定的角色");
                    }
                    info.Role.Name = role.Name;
                }
            }
            //处理部门的验证
            if (EmployeeServiceOption.Option.RequiredDepartment)
            {
                if (info.Department == null)
                {
                    throw ApiException.BadRequest("请指定用户的部门");
                }
                if (info.Department != null && String.IsNullOrWhiteSpace(info.Department.Id))
                {
                    var department = _DepDal.SearchById(info.Department.Id);
                    if (department == null)
                    {
                        throw ApiException.BadRequest("无法找到指定的角色部门");
                    }
                    info.Department.Name = department.Name;
                }
            }
            //扩展的验证处理
            if (EmployeeServiceOption.Option.SavingCheck != null)
            {
                EmployeeServiceOption.Option.SavingCheck(info, logonInfo);
            }

            if (info.IsEmpty())
            {
                if (_Dal.Queryable().Where(e => e.LogonAccount == info.LogonAccount).Count() > 0)
                {
                    throw ApiException.BadRequest("登陆账号已存在，无法新增用户");
                }
            }
            else
            {
                foreach (var item in _Dal.Queryable().Where(e => e.LogonAccount == info.LogonAccount))
                {
                    if (item.Id.ToString() != info.Id.ToString())
                    {
                        throw ApiException.BadRequest("登陆账号已存在，无法修改");
                    }
                }
            }

        }

        /// <summary>
        /// 保存的前置操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void Saving(EmployeeInfo info, TokenLogonInfo logonInfo)
        {
            //设置员工状态
            info.State = UserState.Enable;
            info.ProcessState = ProcessState.Enable;

            //完善数据
            //1、完善部门信息
            if (info.Department != null && !String.IsNullOrWhiteSpace(info.Department.Id))
            {
                string depId = info.Department.Id;
                if (!String.IsNullOrWhiteSpace(depId))
                {
                    DepartmentInfo depInfo = _DepDal.SearchById(depId);
                    if (depInfo == null)
                    {
                        throw ApiException.BadRequest("选择的部门信息不存在，请刷新页面再尝试");
                    }
                    info.Department = new SelectView() { Id = depId, Name = depInfo.Name, Code = depInfo.Code };
                }
            }
            //2、完善角色信息
            if (info.Role != null && !String.IsNullOrWhiteSpace(info.Role.Id))
            {
                string roleId = info.Role.Id;
                if (!String.IsNullOrWhiteSpace(roleId))
                {
                    RoleInfo role = _RoleDal.SearchById(roleId);
                    if (role == null)
                    {
                        throw ApiException.BadRequest("选择的角色信息不存在，请刷新页面再尝试");
                    }
                    info.Role = new SelectView() { Id = roleId, Name = role.Name, Code = "" };
                }
            }

            // 保存的前置验证
            if (EmployeeServiceOption.Option.Saving != null)
            {
                EmployeeServiceOption.Option.Saving(info, logonInfo);
            }
        }

        /// <summary>
        /// 保存的后置处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void Saved(EmployeeInfo info, TokenLogonInfo logonInfo)
        {
            // 保存的前置验证
            if (EmployeeServiceOption.Option.Saved != null)
            {
                EmployeeServiceOption.Option.Saved(info, logonInfo);
            }
        }

        /// <summary>
        /// 更新的后置处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="old"></param>
        /// <param name="logonInfo"></param>
        protected override void Updated(EmployeeInfo info, EmployeeInfo old, TokenLogonInfo logonInfo)
        {
            if (EmployeeServiceOption.Option.Updated != null)
            {
                EmployeeServiceOption.Option.Updated(info, old, logonInfo);
            }
        }
        /// <summary>
        /// 部门信息改变时，修改用户信息
        /// </summary>
        /// <param name="department"></param>
        internal void OnDepartmentChanged(DepartmentInfo department)
        {
            string depId = department.Id.ToString();
            var empList = _Dal.Queryable().Where(e => e.Department != null && e.Department.Id == depId).ToList();
            foreach (var info in empList)
            {
                info.Department.Name = department.Name;
            }
            _Dal.Update(empList);
        }
        /// <summary>
        /// 角色信息改变时，修改用户信息
        /// </summary>
        /// <param name="role"></param>
        internal void OnRoleChanged(RoleInfo role)
        {
            string roleId = role.Id.ToString();
            var empList = _Dal.Queryable().Where(e => e.Role != null && e.Role.Id == roleId).ToList();
            foreach (var info in empList)
            {
                info.Role.Name = role.Name;
            }
            _Dal.Update(empList);
        }
        #endregion

        #region 删除方法
        /// <summary>
        /// 删除时的检查
        /// </summary>
        /// <param name="info"></param>
        protected override void DeleteRemoveCheck(EmployeeInfo info)
        {

        }

        #endregion

        #region 初始化
        /// <summary>
        /// 初始化系统的用户信息  删除已有的用户，设置一个账号密码为a的管理账户
        /// </summary>
        public void Init()
        {
            //删除已有用户
            _Dal.Drop();
            EmployeeInfo emp = new EmployeeInfo();
            emp.LogonAccount = "a";
            emp.LogonPassword = "a";
            emp.Name = "预制管理员";
            emp.ProcessState = ProcessState.Edit;
            emp.State = UserState.Enable;
            emp.CreateDate = DateTime.Now;
            emp.CreaterId = "";
            emp.CreaterName = "系统初始化";
            emp.UpdateDate = DateTime.Now;
            emp.UpdaterId = "";
            emp.UpdaterName = "系统初始化";
            emp.IsDelete = false;
            var role = _RoleDal.Queryable().FirstOrDefault();//初始化时，选择默认的第一个角色
            emp.Role = AutoMapper.Mapper.Map<SelectView>(role);

            _Dal.Insert(emp);
        }
        /// <summary>
        /// 更新预制管理员的配置信息
        /// </summary>
        public void UpdateAdmin()
        {
            var admin = _Dal.Queryable().Where(e => e.Name == "预制管理员").FirstOrDefault();
            if (admin == null)
            {
                admin = new EmployeeInfo();
                admin.LogonAccount = "a";
                admin.LogonPassword = "a";
                admin.Name = "预制管理员";
                admin.ProcessState = ProcessState.Edit;
                admin.State = UserState.Enable;
                admin.CreateDate = DateTime.Now;
                admin.CreaterId = "";
                admin.CreaterName = "系统初始化";
                admin.UpdateDate = DateTime.Now;
                admin.UpdaterId = "";
                admin.UpdaterName = "系统初始化";
                admin.IsDelete = false;
            }
            //设置角色信息
            var role = _RoleDal.Queryable().Where(e => e.Name == "系统管理员").FirstOrDefault();//初始化时，选择默认的第一个角色
            admin.Role = AutoMapper.Mapper.Map<SelectView>(role);
            //保存管理员用户信息
            if (admin.IsEmpty())
            {
                _Dal.Insert(admin);
            }
            else
            {
                _Dal.Update(admin);
            }
        }
        #endregion

        #region 修改登录密码
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public ResultView UpdatePassword(string id, string oldPwd, string newPwd)
        {

            if (String.IsNullOrWhiteSpace(newPwd.Trim()))
            {
                throw ApiException.BadRequest("请输入新密码");
            }
            var emp = _Dal.SearchById(id);
            if (!emp.LogonPassword.Equals(oldPwd))
            {
                throw ApiException.BadRequest("原密码输入错误");
            }
            emp.LogonPassword = newPwd;
            _Dal.Update(emp);
            return ResultView.Success(emp.Id);
        }
        #endregion

        #region 设置员工状态
        /// <summary>
        /// 禁止某些员工登录系统，主要用于员工离职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultView SetDisable(string id)
        {
            var emp = _SearchById(id);
            emp.State = UserState.Disable;
            emp.ProcessState = ProcessState.Disable;
            _Dal.Update(emp);
            return ResultView.Success(id);
        }

        /// <summary>
        /// 恢复某些员工禁止登录系统的状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultView SetEnable(string id)
        {
            var emp = _SearchById(id);
            emp.State = UserState.Enable;
            emp.ProcessState = ProcessState.Enable;
            _Dal.Update(emp);
            return ResultView.Success(id);
        }
        /// <summary>
        /// 解锁某些用户的登录状态 - 用户连续多次由于密码错误而登录失败时，将会为用户设置登录锁状态，本功能用于解除这种登录锁的状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultView SetUnlock(string id)
        {
            var emp = _SearchById(id);
            emp.State = UserState.Enable;
            emp.ProcessState = ProcessState.Enable;
            _Dal.Update(emp);
            return ResultView.Success(id);
        }
        #endregion

        /// <summary>
        /// 查看某角色下的可用员工的个数
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        internal int CountByRoleId(string roleId)
        {
            return _Dal.Queryable().Where(e => e.Role != null && String.IsNullOrEmpty(e.Role.Id) && e.Role.Id == roleId && e.IsDelete == false && e.State == UserState.Enable).Count();
        }
        /// <summary>
        /// 查看某部门下的可用员工的个数
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        internal int CountByDepartmentId(string departmentId)
        {
            return _Dal.Queryable().Where(e => e.Department != null && String.IsNullOrEmpty(e.Department.Id) && e.Department.Id == departmentId && e.IsDelete == false && e.State == UserState.Enable).Count();
        }
    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 员工管理    [ Service ]
    /// </summary>
    public class EmployeeService : MongoBusinessService<EmployeeInfo, EmployeeView, EmployeeQuery>
    {
        #region 构造方法
        private DepartmentDAL _DepDal;
        private RoleDAL _RoleDal;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="servicesProvider"></param>
        /// <param name="depDal"></param>
        /// <param name="roleDal"></param>
        public EmployeeService(EmployeeDAL dal, ILogger<EmployeeService> logger, IServiceProvider servicesProvider,
            DepartmentDAL depDal, RoleDAL roleDal) : base(dal)
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
            var query = _Dal.Queryable();
            //_logger.LogInformation("this is a log test: input.code:{0}, input.key:{1}", input.Name, input.State);
            // var query = _Dal.Queryable();

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
            // 根据查询的关键字查询
            if (!string.IsNullOrWhiteSpace(input.Key))
            {
                query = query.Where(e => (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Key)) ||
                                         (e.Role != null && !string.IsNullOrEmpty(e.Role.Name) && e.Role.Name.Contains(input.Key)) ||
                                         (e.Department != null && !string.IsNullOrEmpty(e.Department.Name) && e.Department.Name.Contains(input.Key)) ||
                                         (!string.IsNullOrEmpty(e.LogonAccount) && e.LogonAccount.Contains(input.Key)));
            }

            // 按名称的模糊查询
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(e => !string.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Name));
            }
            // 按部门ID查询
            if (!string.IsNullOrWhiteSpace(input.DepartmentId))
            {
                query = query.Where(e => !string.IsNullOrEmpty(e.Department.Id) && e.Department.Id == input.DepartmentId);
            }
            //根据根节点查询部门
            if (!string.IsNullOrWhiteSpace(input.RootDepartment))
            {
                query = query.Where(e => e.ParentDepartment != null && e.ParentDepartment.Ids != null && e.ParentDepartment.Ids.Contains(input.RootDepartment));
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
            switch (input.Sort.Property)
            {
                case "name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name); break; }
                case "department.name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Department.Name) : query.OrderByDescending(e => e.Department.Name); break; }
                case "role.name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Role.Name) : query.OrderByDescending(e => e.Role.Name); break; }
                case "state": { query = input.Sort.IsAsc ? query.OrderBy(e => e.State) : query.OrderByDescending(e => e.State); break; }
                case "updateDate": { query = input.Sort.IsAsc ? query.OrderBy(e => e.UpdateDate) : query.OrderByDescending(e => e.UpdateDate); break; }
                default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            }
            #endregion

            //返回查询结果
            return query;
        }
        ///// <summary>
        ///// 获取所有的子部门ID
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //internal List<string> GetSubDepartment(string id)
        //{
        //    List<string> subIds = new List<string>();
        //    var list = _DepDal.Queryable().Where(e => e.ParentId == id).ToList();
        //    foreach (var item in list)
        //    {
        //        string itemId = item.Id.ToString();
        //        subIds.Add(itemId);
        //        subIds.AddRange(GetSubDepartment(itemId));
        //    }
        //    return subIds;
        //}

        /// <summary>
        /// 获取所有可用的员工列表，按部门分组
        /// </summary>
        /// <returns></returns>
        public List<EmployeeGroupByDepartment> GetEmployeeByDepartment()
        {
            var employeeList = _Dal.Queryable().Where(e => e.IsDelete == false).ToList();
            var depList = ServiceLoader.GetService<DepartmentService>().SearchQueryable().ToList();
            List<EmployeeGroupByDepartment> result = new List<EmployeeGroupByDepartment>();
            foreach (var item in employeeList.GroupBy(e => e.Department.Id))
            {
                var dep = depList.Where(e => e.IdString == item.Key).FirstOrDefault();
                result.Add(new EmployeeGroupByDepartment()
                {
                    Id = item.Key,
                    Name = dep.Name,
                    Employees = AutoMapper.Mapper.Map<List<SelectView>>(item.ToList())
                });
            }

            return result;
        }
        #endregion

        #region 新增 / 修改方法
        /// <summary>
        /// 设置角色是否必填
        /// </summary>
        public static bool RequiredRole { get; set; } = true;

        /// <summary>
        /// 设置部门是否必填
        /// </summary>
        public static bool RequiredDepartment { get; set; } = true;

        /// <summary>
        /// 保存的校验
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void SavingCheck(EmployeeInfo info, TokenLogonInfo logonInfo)
        {
            // 处理角色的验证
            if (RequiredRole)
            {
                if (info.Role == null)
                {
                    throw ApiException.BadRequest("请指定用户的角色");
                }
            }

            // 处理部门的验证
            if (RequiredDepartment)
            {
                if (info.Department == null)
                {
                    throw ApiException.BadRequest("请指定用户的部门");
                }

            }

            // 验证登录账号
            if (info.IsEmpty)
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
            if (info.Department != null && !string.IsNullOrWhiteSpace(info.Department.Id))
            {
                string depId = info.Department.Id;
                if (!string.IsNullOrWhiteSpace(depId))
                {
                    DepartmentInfo depInfo = _DepDal.SearchById(depId);
                    if (depInfo == null)
                    {
                        throw ApiException.BadRequest("选择的部门信息不存在，请刷新页面再尝试");
                    }
                    info.Department = new SelectView() { Id = depId, Name = depInfo.Name, Code = depInfo.Code };
                    info.ParentDepartment.Id = depInfo.ParentId;
                    info.ParentDepartment.Name = depInfo.Name;
                    info.ParentDepartment.Ids = depInfo.ParentsIds;
                }
            }

            //2、完善角色信息
            if (info.Role != null && !string.IsNullOrWhiteSpace(info.Role.Id))
            {
                string roleId = info.Role.Id;
                if (!string.IsNullOrWhiteSpace(roleId))
                {
                    RoleInfo role = _RoleDal.SearchById(roleId);
                    if (role == null)
                    {
                        throw ApiException.BadRequest("选择的角色信息不存在，请刷新页面再尝试");
                    }
                    info.Role = new SelectView() { Id = roleId, Name = role.Name, Code = "" };
                }
            }
        }
        #endregion

        #region 部门信息修改时的事件处理
        /// <summary>
        /// 部门更新的后置处理
        /// </summary>
        /// <param name="depInfo">新的部门信息</param>
        /// <param name="oldInfo">旧的部门信息</param>
        /// <param name="logonInfo"></param>
        internal static void OnDepartmentUpdated(DepartmentInfo depInfo, DepartmentInfo oldInfo, TokenLogonInfo logonInfo)
        {
            // 更新员工信息
            ThreadPool.QueueUserWorkItem(h =>
            {
                EmployeeService service = ServiceLoader.GetService<EmployeeService>();
                // 更新所有的员工信息
                service.UpdateByDepartmentInfo(depInfo, oldInfo);
            });
        }
        /// <summary>
        /// 部门信息改变，更新所有的员工信息
        /// </summary>
        /// <param name="depInfo"></param>
        /// <param name="oldInfo"></param>
        private void UpdateByDepartmentInfo(DepartmentInfo depInfo, DepartmentInfo oldInfo)
        {
            // 部门信息改变，更新所有的员工信息
            if (depInfo.Name != oldInfo.Name || depInfo.ParentId != oldInfo.ParentId ||
                depInfo.ParentName != oldInfo.ParentName || depInfo.ParentsIds.Equals(oldInfo.ParentsIds))
            {
                ((EmployeeDAL)_Dal).UpdateDepartmentInfo(depInfo);
            }
        }
        #endregion

        #region 角色信息修改时的事件处理
        /// <summary>
        /// 角色更新的后置处理
        /// </summary>
        /// <param name="roleInfo">新的角色信息</param>
        /// <param name="oldInfo">旧的角色信息</param>
        /// <param name="logonInfo"></param>
        internal static void OnRoleUpdated(RoleInfo roleInfo, RoleInfo oldInfo, TokenLogonInfo logonInfo)
        {
            if (roleInfo.Name != oldInfo.Name)
            {
                // 更新员工信息
                ThreadPool.QueueUserWorkItem(h =>
                {
                    EmployeeDAL dal = ServiceLoader.GetService<EmployeeDAL>();
                    // 更新部门信息
                    dal.UpdateRoleInfo(roleInfo);
                });
            }
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
            EmployeeInfo emp = new EmployeeInfo()
            {
                LogonAccount = "a",
                LogonPassword = "a",
                Name = "预制管理员",
                ProcessState = ProcessState.Edit,
                State = UserState.Enable,
                CreateDate = DateTime.Now,
                CreaterId = "",
                CreaterName = "系统初始化",
                UpdateDate = DateTime.Now,
                UpdaterId = "",
                UpdaterName = "系统初始化",
                IsDelete = false
            };
            var role = _RoleDal.Queryable().FirstOrDefault();//初始化时，选择默认的第一个角色
            emp.Role = AutoMapper.Mapper.Map<SelectView>(role);
            _Dal.InsertObject(emp);
        }
        /// <summary>
        /// 更新预制管理员的配置信息
        /// </summary>
        public void UpdateAdmin()
        {
            var admin = _Dal.Queryable().Where(e => e.Name == "预制管理员").FirstOrDefault();
            if (admin == null)
            {
                admin = new EmployeeInfo()
                {
                    LogonAccount = "a",
                    LogonPassword = "a",
                    Name = "预制管理员",
                    ProcessState = ProcessState.Edit,
                    State = UserState.Enable,
                    CreateDate = DateTime.Now,
                    CreaterId = "",
                    CreaterName = "系统初始化",
                    UpdateDate = DateTime.Now,
                    UpdaterId = "",
                    UpdaterName = "系统初始化",
                    IsDelete = false,
                    Department = new SelectView() { Code = "", Name = "", Id = "" }
                };
            }
            //设置角色信息
            var role = _RoleDal.Queryable().Where(e => e.Name == "系统管理员").FirstOrDefault();//初始化时，选择默认的第一个角色
            admin.Role = AutoMapper.Mapper.Map<SelectView>(role);
            //admin.UpdateDate = DateTime.Now;
            //保存管理员用户信息
            if (admin.IsEmpty)
            {
                _Dal.InsertObject(admin);
            }
            else
            {
                _Dal.UpdateObject(admin);
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

            if (string.IsNullOrWhiteSpace(newPwd.Trim()))
            {
                throw ApiException.BadRequest("请输入新密码");
            }
            var emp = _Dal.SearchById(id);
            if (!emp.LogonPassword.Equals(oldPwd))
            {
                throw ApiException.BadRequest("原密码输入错误");
            }
            emp.LogonPassword = newPwd;
            _Dal.UpdateObject(emp);
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
            _Dal.UpdateObject(emp);
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
            _Dal.UpdateObject(emp);
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
            _Dal.UpdateObject(emp);
            return ResultView.Success(id);
        }
        #endregion

        #region 数量统计
        /// <summary>
        /// 查看某角色下的可用员工的个数
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        internal int CountByRoleId(string roleId)
        {
            return _Dal.Queryable().Where(e => e.Role != null && string.IsNullOrEmpty(e.Role.Id) && e.Role.Id == roleId && e.IsDelete == false && e.State == UserState.Enable).Count();
        }
        /// <summary>
        /// 查看某部门下的可用员工的个数
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        internal int CountByDepartmentId(string departmentId)
        {
            return _Dal.Queryable().Where(e => e.Department != null && string.IsNullOrEmpty(e.Department.Id) && e.Department.Id == departmentId && e.IsDelete == false && e.State == UserState.Enable).Count();
        }
        #endregion

        #region 修改所有人的登陆密码
        /// <summary>
        /// 更新所有人的密码
        /// </summary>
        public void UpdateAllPassword(string password = "")
        {
            foreach (var employee in _Dal.Queryable())
            {
                employee.LogonPassword = string.IsNullOrWhiteSpace(password) ? Guid.NewGuid().ToString("N").Substring(0, 8) : password;
                _Dal.UpdateObject(employee);
            }
        }
        #endregion
    }
}

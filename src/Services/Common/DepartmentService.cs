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
    /// 部门管理    [ Service ]
    /// </summary>
    public class DepartmentService : MongoBusinessService<DepartmentInfo, DepartmentView, DepartmentQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        public DepartmentService(DepartmentDAL dal) : base(dal)
        {

        }

        #endregion

        #region 查询方法
        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override IQueryable<DepartmentInfo> _Filter(DepartmentQuery filter)
        {
            var query = _Dal.Queryable();

            #region 查询条件
            //不显示删除的数据
            query = query.Where(e => e.IsDelete == false);

            // 按名称的模糊查询
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(e => !string.IsNullOrEmpty(e.Name) && e.Name.Contains(filter.Name));
            }
            // 按上级部门ID查询
            if (!string.IsNullOrWhiteSpace(filter.ParentId))
            {
                query = query.Where(e => e.ParentsIds.Contains(filter.ParentId));
            }
            #endregion

            #region 设置排序规则
            //设置排序方式
            switch (filter.Sort.Property)
            {
                case "name": { query = filter.Sort.IsAsc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name); break; }
                case "code": { query = filter.Sort.IsAsc ? query.OrderBy(e => e.Code) : query.OrderByDescending(e => e.Code); break; }
                case "parent": { query = filter.Sort.IsAsc ? query.OrderBy(e => e.ParentName) : query.OrderByDescending(e => e.ParentName); break; }
                case "index": { query = filter.Sort.IsAsc ? query.OrderBy(e => e.Index) : query.OrderByDescending(e => e.Index); break; }
                case "date": { query = filter.Sort.IsAsc ? query.OrderBy(e => e.UpdateDate) : query.OrderByDescending(e => e.UpdateDate); break; }
                default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            }
            #endregion

            //返回查询结果
            return query;
        }

        /// <summary>
        /// 获取所有的子部门ID（包含指定部门，及逻辑删除的子部门）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetSubDepartmentId(string id)
        {
            var list = _Dal.Queryable().Where(e => e.ParentsIds.Contains(id))
                                   .Select(e => e.Id).ToList();
            return AutoMapper.Mapper.Map<List<string>>(list);
        }
        ///// <summary>
        ///// 获取子部门列表（包含指定部门）
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public List<BaseViewModel> GetSubView(string id)
        //{
        //    return _Dal.Queryable().Where(e => e.ParentsIds.Contains(id))
        //               .Select(e => new BaseViewModel() { Id = e.Id.ToString(), Name = e.Name }).ToList();
        //}

        /// <summary>
        /// 获取子部门对象列表 （包含指定部门）
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<DepartmentInfo> GetSub(string departmentId)
        {
            return _Dal.Queryable().Where(e => e.ParentsIds.Contains(departmentId)).ToList();
        }
        /// <summary>
        /// 获取根部门
        /// </summary>
        /// <returns></returns>
        public List<DepartmentInfo> GetRoot()
        {
            return _Dal.Queryable().Where(e => string.IsNullOrEmpty(e.ParentId)).ToList();
        }
        #endregion

        #region 新增 / 修改方法
        /// <summary>
        /// 保存的校验
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void SavingCheck(DepartmentInfo info, TokenLogonInfo logonInfo)
        {
            //数据验证
            if (string.IsNullOrWhiteSpace(info.Name))
            {
                throw ApiException.BadRequest("请指定部门的名称");
            }
        }
        /// <summary>
        /// 保存前，完善数据
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void Saving(DepartmentInfo info, TokenLogonInfo logonInfo)
        {
            //完善上级部门名称
            if (!string.IsNullOrEmpty(info.ParentId))
            {
                var parent = _SearchById(info.ParentId);
                info.ParentName = parent.Name;
                info.ParentsIds = parent.ParentsIds;
                if (!info.IsEmpty)
                {
                    info.ParentsIds.Add(info.Id.ToString());
                }
            }
            else
            {
                info.ParentId = string.Empty;
                info.ParentName = string.Empty;
                if (info.IsEmpty)
                {
                    info.ParentsIds = new List<string>();
                }
                else
                {
                    info.ParentsIds = new List<string>() { info.Id.ToString() };
                }
            }

            //完善部门主管信息
            //if (!string.IsNullOrEmpty(info.ManageId))
            //{
            //    EmployeeService employeeService = ServiceLoader.GetService<EmployeeService>();
            //    var employee = employeeService._SearchById(info.ManageId);
            //    info.ManageName = employee.Name;
            //}
            //else
            //{
            //    info.ManageName = string.Empty;
            //}
            // 上级部门不能是自己的子部门
            if (!string.IsNullOrEmpty(info.ParentId) && GetSubDepartmentId(info.Id.ToString()).Contains(info.ParentId))
            {
                throw ApiException.BadRequest("上级部门不能是" + info.Name + "的子部门");
            }
        }
        /// <summary>
        /// 新增后置操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void Created(DepartmentInfo info, TokenLogonInfo logonInfo)
        {
            info.ParentsIds.Add(info.Id.ToString());
            _Dal.UpdateObject(info);
        }
        /// <summary>
        /// 更新的后置操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="old"></param>
        /// <param name="logonInfo"></param>
        protected override void Updated(DepartmentInfo info, DepartmentInfo old, TokenLogonInfo logonInfo)
        {
            // 只针对部门名称进行修改
            if (info.Name != old.Name)
            {
                ThreadPool.QueueUserWorkItem(h =>
                {
                    // 如果当前部门名称改变，会同时修改其子部门下的关联上级部门名称
                    ((DepartmentDAL)_Dal).UpdateParentsDepartmentName(info);
                });
            }
            // 如果上级部门改变，同时修改下级的上级列表
            if (info.ParentId != old.ParentId)
            {
                ThreadPool.QueueUserWorkItem(h =>
                {
                    foreach (var dep in _Dal.Queryable().Where(e => e.ParentId == info.Id.ToString()))
                    {
                        Update(dep, logonInfo);
                    }
                });
            }
        }
        #endregion

        #region 删除方法
        /// <summary>
        /// 删除时的检查
        /// </summary>
        /// <param name="info"></param>
        protected override void DeleteRemoveCheck(DepartmentInfo info)
        {
            string depId = info.Id.ToString();
            //如果有下级部门不允许删除
            if (_Dal.Queryable().Where(e => e.ParentId == depId).Count() > 0)
            {
                throw ApiException.RemoveUsed("删除的部门下有子部门信息，不允许删除。");
            }
            //如果部门下有员工不允许删除
            EmployeeService employeeService = ServiceLoader.GetService<EmployeeService>();
            if (employeeService.CountByDepartmentId(depId) > 0)
            {
                throw ApiException.RemoveUsed("删除的部门下有员工信息，不允许删除。");
            }
        }
        #endregion

        #region 部门内的员工处理
        ///// <summary>
        ///// 判断指定用户是否为部门的管理员
        ///// </summary>
        ///// <param name="employeeId"></param>
        ///// <returns>如果为管理员，返回部门信息，否则返回空对象</returns>
        //public DepartmentInfo IsManager(string employeeId)
        //{
        //    return _Dal.Queryable().Where(e => e.ManageId == employeeId).FirstOrDefault();
        //}

        /// <summary>
        /// 判断指定的用户是否属于指定的部门(可以是子部门)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="departmentId"></param>
        /// <param name="employeeInfo">返回用户对象</param>
        /// <returns>是否属于指定的部门下</returns>
        public bool HasEmployee(string employeeId, string departmentId, out EmployeeInfo employeeInfo)
        {
            // 获取用户信息
            EmployeeService employeeService = ServiceLoader.GetService<EmployeeService>();
            employeeInfo = employeeService._SearchById(employeeId);
            // 判断用户是否在指定的部门下
            if (employeeInfo == null || employeeInfo.Department == null || string.IsNullOrWhiteSpace(employeeInfo.Department.Id))
            {
                return false;
            }
            // 获取所有的子部门列表
            List<string> allDepIds = GetSubDepartmentId(departmentId);
            return allDepIds.Contains(employeeInfo.Department.Id.ToString());
        }
        #endregion
    }
}

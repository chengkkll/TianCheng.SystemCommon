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
    /// 部门管理    [ Service ]
    /// </summary>
    public class DepartmentService : BusinessService<DepartmentInfo, DepartmentView, DepartmentQuery>
    {
        #region 构造方法
        EmployeeService _EmployeeService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="employeeService"></param>
        public DepartmentService(DepartmentDAL dal, ILogger<DepartmentService> logger,
            EmployeeService employeeService) : base(dal, logger)
        {
            _EmployeeService = employeeService;
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 根据id查询对象信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override DepartmentView SearchById(string id)
        {
            //根据ID获取员工信息
            var info = _SearchById(id);
            var view = AutoMapper.Mapper.Map<DepartmentView>(info);

            //获取部门下的员工信息列表
            EmployeeQuery empQuery = new EmployeeQuery() { DepartmentId = id };
            view.Employees = _EmployeeService.Select(empQuery);

            //返回
            return view;
        }

        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<DepartmentInfo> _Filter(DepartmentQuery input)
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
            // 按上级部门ID查询
            if (!string.IsNullOrWhiteSpace(input.ParentId))
            {
                query = query.Where(e => e.ParentId == input.ParentId);
            }
            // 按部门主管ID查询
            if (!string.IsNullOrWhiteSpace(input.ManageId))
            {
                query = query.Where(e => e.ManageId == input.ManageId);
            }
            // 按部门主管的名称模糊查询
            if (!string.IsNullOrWhiteSpace(input.ManageName))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.ManageName) && e.ManageName.Contains(input.ManageName));
            }
            // 按所属行业id列表查询
            if (input.Industries != null)
            {
                query = query.Where(e => e.Industries != null && e.Industries.Any(i => input.Industries.Contains(i.Id)));
            }

            #endregion

            #region 设置排序规则

            //设置排序方式
            switch (input.OrderBy)
            {
                case "nameAsc": { query = query.OrderBy(e => e.Name); break; }
                case "nameDesc": { query = query.OrderByDescending(e => e.Name); break; }
                case "codeAsc": { query = query.OrderBy(e => e.Code); break; }
                case "codeDesc": { query = query.OrderByDescending(e => e.Code); break; }
                case "parentAsc": { query = query.OrderBy(e => e.ParentName); break; }
                case "parentDesc": { query = query.OrderByDescending(e => e.ParentName); break; }
                case "manageAsc": { query = query.OrderBy(e => e.ManageName); break; }
                case "manageDesc": { query = query.OrderByDescending(e => e.ManageName); break; }
                case "dateAsc": { query = query.OrderBy(e => e.UpdateDate); break; }
                case "dateDesc": { query = query.OrderByDescending(e => e.UpdateDate); break; }
                case "indexAsc": { query = query.OrderBy(e => e.Index); break; }
                case "indexDesc": { query = query.OrderByDescending(e => e.Index); break; }
                default: { query = query.OrderBy(e => e.Index); break; }
            }
            #endregion

            //返回查询结果
            return query;
        }

        /// <summary>
        /// 获取一个以部门名称为key的字典信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, DepartmentInfo> GetNameDict()
        {
            Dictionary<string, DepartmentInfo> dict = new Dictionary<string, DepartmentInfo>();
            foreach (DepartmentInfo info in _Dal.SearchQueryable().Where(e => e.IsDelete == false))
            {
                if (!dict.ContainsKey(info.Name))
                {
                    dict.Add(info.Name, info);
                }
            }
            return dict;
        }

        /// <summary>
        /// 获取所有的子部门ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetSubDepartmentId(string id)
        {
            List<string> subIds = new List<string>();
            var list = _Dal.SearchQueryable().Where(e => e.ParentId == id).ToList();
            foreach (var item in list)
            {
                string itemId = item.Id.ToString();
                subIds.Add(itemId);
                subIds.AddRange(GetSubDepartmentId(itemId));
            }
            return subIds;
        }
        /// <summary>
        /// 获取所有子部门ID和名称列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BaseViewModel> GetSubDepartmentView(string id)
        {
            List<BaseViewModel> subView = new List<BaseViewModel>();
            var list = _Dal.SearchQueryable().Where(e => e.ParentId == id).ToList();
            foreach (var item in list)
            {
                string itemId = item.Id.ToString();
                subView.Add(new BaseViewModel() { Id = itemId, Name = item.Name });
                subView.AddRange(GetSubDepartmentView(itemId));
            }
            return subView;
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
            if (String.IsNullOrWhiteSpace(info.Name))
            {
                throw ApiException.BadRequest("请指定部门的名称");
            }

            //扩展的验证处理
            if (DepartmentServiceOption.Option.SavingCheck != null)
            {
                DepartmentServiceOption.Option.SavingCheck(info, logonInfo);
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
            if (!String.IsNullOrEmpty(info.ParentId))
            {
                var parent = _SearchById(info.ParentId);
                info.ParentName = parent.Name;
            }
            else
            {
                info.ParentName = String.Empty;
            }
            //完善部门主管信息
            if (!String.IsNullOrEmpty(info.ManageId))
            {
                var employee = _EmployeeService._SearchById(info.ManageId);
                info.ManageName = employee.Name;
            }
            else
            {
                info.ManageName = String.Empty;
            }
            //上级部门不能是自己的子部门
            if (!String.IsNullOrEmpty(info.ParentId) && GetSubDepartmentId(info.Id.ToString()).Contains(info.ParentId))
            {
                throw ApiException.BadRequest("上级部门不能是" + info.Name + "的子部门");
            }
        }

        /// <summary>
        /// 更新的后置操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="old"></param>
        /// <param name="logonInfo"></param>
        protected override void Updated(DepartmentInfo info, DepartmentInfo old, TokenLogonInfo logonInfo)
        {
            if (!info.Name.Equals(old.Name))
            {
                //如果当前部门名称改变，会同时修改其子部门下的关联上级部门名称
                string id = info.Id.ToString();
                string name = info.Name;
                var query = _Dal.SearchQueryable().Where(e => e.ParentId == id).ToList();
                if (query != null && query.Count != 0)
                {
                    foreach (var item in query)
                    {
                        item.ParentName = name;
                    }
                    _Dal.Save(query);
                }
                //当前部门名称改变，查询员工信息中的部门信息，并改变。
                _EmployeeService.OnDepartmentChanged(info);
            }
            if (DepartmentServiceOption.Option.Updated != null)
            {
                DepartmentServiceOption.Option.Updated(info, old, logonInfo);
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
            if (_Dal.SearchQueryable().Where(e => e.ParentId == depId).Count() > 0)
            {
                throw ApiException.RemoveUsed("删除的部门下有子部门信息，不允许删除。");
            }
            //如果部门下有员工不允许删除
            if (_EmployeeService.CountByDepartmentId(depId) > 0)
            {
                throw ApiException.RemoveUsed("删除的部门下有员工信息，不允许删除。");
            }
        }

        #endregion

        #region 部门内的员工处理
        /// <summary>
        /// 判断指定用户是否为部门的管理员
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>如果为管理员，返回部门信息，否则返回空对象</returns>
        public DepartmentInfo IsManager(string employeeId)
        {
            return _Dal.SearchQueryable().Where(e => e.ManageId == employeeId).FirstOrDefault();
        }

        /// <summary>
        /// 判断指定的用户是否属于指定的部门(可以是子部门)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="departmentId"></param>
        /// <param name="employeeInfo"></param>
        /// <returns></returns>
        public bool HasEmployee(string employeeId, string departmentId, out EmployeeInfo employeeInfo)
        {
            //获取所有的子部门列表
            List<string> allDepIds = GetSubDepartmentId(departmentId);
            allDepIds.Add(departmentId);
            //获取用户信息
            employeeInfo = _EmployeeService._SearchById(employeeId);
            //判断用户是否在指定的部门下
            if (employeeInfo == null || employeeInfo.Department == null)
            {
                return false;
            }
            return allDepIds.Contains(employeeInfo.Department.Id.ToString());
        }
        #endregion
    }
}

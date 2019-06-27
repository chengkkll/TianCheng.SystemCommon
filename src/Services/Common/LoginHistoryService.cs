using Microsoft.AspNetCore.Http;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 登录记录管理    [ Service ]
    /// </summary>
    public class LoginHistoryService : MongoBusinessService<LoginHistoryInfo, LoginHistoryView, LoginHistoryQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        public LoginHistoryService(LoginHistoryDAL dal) : base(dal)
        {
        }
        #endregion


        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<LoginHistoryInfo> _Filter(LoginHistoryQuery input)
        {
            var query = _Dal.Queryable();
            //_logger.LogInformation("this is a log test: input.code:{0}, input.key:{1}", input.Name, input.State);
            // var query = _Dal.Queryable();

            #region 查询条件
            // 根据查询的关键字查询
            if (!string.IsNullOrWhiteSpace(input.Key))
            {
                query = query.Where(e => (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Key)) ||
                                         (!string.IsNullOrEmpty(e.DepartmentName) && e.DepartmentName.Contains(input.Key)) ||
                                         (!string.IsNullOrEmpty(e.IpAddressCityName) && e.IpAddressCityName.Contains(input.Key)) ||
                                         (!string.IsNullOrEmpty(e.IpAddress) && e.IpAddress.Contains(input.Key)));
            }
            #endregion

            #region 设置排序规则
            switch (input.Sort.Property)
            {
                case "name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name); break; }
                case "departmentName": { query = input.Sort.IsAsc ? query.OrderBy(e => e.DepartmentName) : query.OrderByDescending(e => e.DepartmentName); break; }
                case "ipAddress": { query = input.Sort.IsAsc ? query.OrderBy(e => e.IpAddress) : query.OrderByDescending(e => e.IpAddress); break; }
                case "date": { query = input.Sort.IsAsc ? query.OrderBy(e => e.CreateDate) : query.OrderByDescending(e => e.CreateDate); break; }
                default: { query = query.OrderByDescending(e => e.CreateDate); break; }
            }
            #endregion

            //返回查询结果
            return query;
        }

        /// <summary>
        /// 登录事件处理
        /// </summary>
        /// <param name="employee"></param>
        internal static void OnLogin(EmployeeInfo employee)
        {
            // 1、记录登录信息
            // 2、如果登录用户需要验证IP地址，验证
            if (employee.LoginType == LoginVerifierType.IpAddress)
            {
                // 获取用户所在部门的IP地址范围
                // 比较用户的IP地址
                IHttpContextAccessor accessor = ServiceLoader.GetService<IHttpContextAccessor>();
                string userIp = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            if (employee.LoginType == LoginVerifierType.ShortRange)
            {

            }
        }
    }
}

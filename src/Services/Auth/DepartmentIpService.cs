using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services.Auth
{
    /// <summary>
    /// 部门IP服务
    /// </summary>
    public class DepartmentIpService : MongoBusinessService<DepartmentIpInfo, QueryInfo>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        public DepartmentIpService(DepartmentIpDAL dal) : base(dal)
        {
        }

        #region 查询方法
        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<DepartmentIpInfo> _Filter(QueryInfo input)
        {
            var query = _Dal.Queryable();

            #region 查询条件
            //不显示删除的数据
            query = query.Where(e => e.IsDelete == false);
            #endregion

            #region 设置排序规则

            //设置排序方式
            switch (input.Sort.Property)
            {
                case "name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.DepartmentName) : query.OrderByDescending(e => e.DepartmentName); break; }
                case "date": { query = input.Sort.IsAsc ? query.OrderBy(e => e.UpdateDate) : query.OrderByDescending(e => e.UpdateDate); break; }
                default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            }
            #endregion

            //返回查询结果
            return query;
        }
        #endregion

    }
}

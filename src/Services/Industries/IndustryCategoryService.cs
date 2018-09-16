using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 行业分类  [ Service ]
    /// </summary>
    public class IndustryCategoryService : MongoBusinessService<IndustryCategoryInfo, IndustryCategoryView, IndustryCategoryQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        /// <param name="servicesProvider"></param>
        public IndustryCategoryService(IndustryCategoryDAL dal, ILogger<IndustryCategoryService> logger, IServiceProvider servicesProvider)
            : base(dal, logger, servicesProvider)
        {
        }
        #endregion

        #region 查询
        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<IndustryCategoryInfo> _Filter(IndustryCategoryQuery input)
        {
            var query = _Dal.Queryable();

            #region 查询条件
            //不显示删除的数据
            query = query.Where(e => e.IsDelete == false);

            // 按名称的模糊查询
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(e => !String.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Name) ||
                                         e.Industries != null && e.Industries.Any(i => e.Name.Contains(input.Name)));
            }
            #endregion

            #region 设置排序规则

            //设置排序方式
            switch (input.Sort.Property)
            {
                case "name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name); break; }
                case "date": { query = input.Sort.IsAsc ? query.OrderBy(e => e.UpdateDate) : query.OrderByDescending(e => e.UpdateDate); break; }
                default: { query = query.OrderByDescending(e => e.UpdateDate); break; }
            }
            #endregion

            //返回查询结果
            return query;
        }
        #endregion

        #region 新增 / 修改方法
        /// <summary>
        /// 保存的校验
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void SavingCheck(IndustryCategoryInfo info, TokenLogonInfo logonInfo)
        {
            //数据验证
            if (String.IsNullOrWhiteSpace(info.Name))
            {
                throw ApiException.BadRequest("行业分类名称不能为空");
            }

            //检查行业名称不能重复
            var query = _Dal.Queryable().Where(e => !String.IsNullOrEmpty(e.Name) && e.Name.Equals(info.Name) && e.IsDelete == false);

            if (info.IsEmpty)
            {
                if (query.Count() > 0)
                {
                    throw ApiException.BadRequest("行业分类不能重复");
                }
            }
            else
            {
                string id = info.Id.ToString();
                foreach (var item in query)
                {
                    if (item.Id.ToString() != id)
                    {
                        throw ApiException.BadRequest("行业分类不能重复");
                    }
                }
            }
        }
        #endregion
    }
}

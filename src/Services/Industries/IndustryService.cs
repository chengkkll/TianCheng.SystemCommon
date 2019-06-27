using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 行业信息
    /// </summary>
    public class IndustryService : MongoBusinessService<IndustryInfo, IndustryView, IndustryQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="logger"></param>
        public IndustryService(IndustryDAL dal, ILogger<IndustryService> logger) : base(dal)
        {

        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 根据编码列表查询行业
        /// </summary>
        /// <param name="codeList"></param>
        /// <returns></returns>
        public List<IndustryInfo> SearchByCodeList(List<string> codeList)
        {
            return _Dal.Queryable().Where(e => codeList.Contains(e.Code)).ToList();
        }

        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<IndustryInfo> _Filter(IndustryQuery input)
        {
            var query = _Dal.Queryable();

            #region 查询条件

            // 按编码、名称、说明模糊查询
            if (!string.IsNullOrWhiteSpace(input.Key))
            {
                query = query.Where(e => (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Key)) ||
                                         (!string.IsNullOrEmpty(e.Code) && e.Code.Contains(input.Key)) ||
                                         (!string.IsNullOrEmpty(e.Remarks) && e.Remarks.Contains(input.Key)));
            }

            // 根据行业ID列表
            if (input.IndustryIdList != null && input.IndustryIdList.Count > 0)
            {
                List<MongoDB.Bson.ObjectId> IdList = new List<MongoDB.Bson.ObjectId>();
                foreach (string id in input.IndustryIdList)
                {
                    if (MongoDB.Bson.ObjectId.TryParse(id, out MongoDB.Bson.ObjectId objId))
                    {
                        IdList.Add(objId);
                    }
                }

                query = query.Where(e => IdList.Contains(e.Id));
            }

            #endregion

            #region 设置排序规则
            //设置排序方式
            switch (input.Sort.Property)
            {
                case "code": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Code) : query.OrderByDescending(e => e.Code); break; }
                case "name": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name); break; }
                case "remarks": { query = input.Sort.IsAsc ? query.OrderBy(e => e.Remarks) : query.OrderByDescending(e => e.Remarks); break; }
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
        /// 保存验证处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        protected override void SavingCheck(IndustryInfo info, TokenLogonInfo logonInfo)
        {
            //行业名称不能为空
            if (string.IsNullOrWhiteSpace(info.Name))
            {
                throw ApiException.BadRequest("行业名称不能为空");
            }
            //行业名称不能重复
            var query = _Dal.Queryable().Where(e => e.Name == info.Name).ToList();
            if (!info.IsEmpty)//修改时过滤本身的ID
            {
                string id = info.Id.ToString();
                for (int i = 0; i < query.Count; i++)
                {
                    if (query[i].Id.ToString() == id)
                    {
                        query.RemoveAt(i);
                        break;
                    }
                }
            }
            if (query.Count() > 0)
            {
                throw ApiException.BadRequest("行业名称不能重复");
            }
        }
        #endregion
    }
}

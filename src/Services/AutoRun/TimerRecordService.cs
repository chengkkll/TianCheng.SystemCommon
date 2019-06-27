using System;
using System.Linq;
using TianCheng.BaseService;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 定时运行记录 [ Service ]
    /// </summary>
    public class TimerRecordService : MongoBusinessService<TimerRecordInfo, TimerRecordView, TimerRecordQuery>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        public TimerRecordService(TimerRecordDAL dal) : base(dal)
        {

        }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info"></param>
        public void Append(TimerRecordInfo info)
        {
            info.RunTime = DateTime.Now;
            info.CreateDate = DateTime.Now;
            info.UpdateDate = DateTime.Now;
            _Dal.InsertObject(info);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        public void Update(TimerRecordInfo info)
        {
            info.UpdateDate = DateTime.Now;
            info.EndTime = DateTime.Now;
            _Dal.UpdateObject(info);
        }
        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override IQueryable<TimerRecordInfo> _Filter(TimerRecordQuery input)
        {
            var query = _Dal.Queryable();

            #region 查询条件
            // 按定时器名称模糊查询
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(e => !string.IsNullOrEmpty(e.Name) && e.Name.Contains(input.Name));
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
    }
}

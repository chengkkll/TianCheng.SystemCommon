using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianCheng.DAL;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 部门信息 [数据持久化]
    /// </summary>
    [DBMapping("System_DepartmentInfo")]
    public class DepartmentDAL : MongoOperation<DepartmentInfo>
    {
        ///// <summary>
        ///// 获取指定部门及其下级部门
        ///// </summary>
        ///// <param name="id">部门ID</param>
        //public List<DepartmentInfo> GetSelfAndSub(string id)
        //{
        //    var builder = Builders<DepartmentInfo>.Filter;
        //    FilterDefinition<DepartmentInfo> filter = builder.Eq("ParentsIds", id) | builder.Eq("_id", new ObjectId(id));

        //    try
        //    {
        //        return _mongoCollection.Find(filter).ToList();
        //    }
        //    catch (System.TimeoutException te)
        //    {
        //        DBLog.Logger.LogWarning(te, "数据库链接超时。链接字符串：" + _options.ConnectionOptions.ConnectionString());
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        DBLog.Logger.LogWarning(ex, "操作异常终止。");
        //        throw;
        //    }
        //}

        /// <summary>
        /// 批量更新上级部门名称
        /// </summary>
        /// <param name="departmentInfo"></param>
        public void UpdateParentsDepartmentName(DepartmentInfo departmentInfo)
        {
            // 验证参数
            if (departmentInfo.IsEmpty)
            {
                return;
            }
            // 设置查询条件
            FilterDefinition<DepartmentInfo> filter = Builders<DepartmentInfo>.Filter.Eq("ParentId", departmentInfo.Id.ToString());
            // 设置更新内容
            UpdateDefinition<DepartmentInfo> ud = Builders<DepartmentInfo>.Update.Set("ParentName", departmentInfo.Name);
            try
            {
                UpdateResult result = _mongoCollection.UpdateMany(filter, ud);
            }
            catch (System.TimeoutException te)
            {
                DBLog.Logger.LogWarning(te, "数据库链接超时。链接字符串：" + _options.ConnectionOptions.ConnectionString());
                throw;
            }
            catch (Exception ex)
            {
                DBLog.Logger.LogWarning(ex, "操作异常终止。");
                throw;
            }
        }
    }
}

using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 登录记录 [数据持久化]
    /// </summary>
    [DBMapping("System_RoleInfo")]
    public class LoginHistoryDAL : MongoOperation<LoginHistoryInfo>
    {
    }
}

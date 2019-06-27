using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 角色信息 [数据持久化]
    /// </summary>
    [DBMapping("system_role")]
    public class RoleDAL : MongoOperation<RoleInfo>
    {
    }
}

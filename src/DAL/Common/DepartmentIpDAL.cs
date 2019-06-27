using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 部门可用IP范围 [数据持久化]
    /// </summary>
    [DBMapping("system_auth_department_ip")]
    public class DepartmentIpDAL : MongoOperation<DepartmentIpInfo>
    {
    }
}

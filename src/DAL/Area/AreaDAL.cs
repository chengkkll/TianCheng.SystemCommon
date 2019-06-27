using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 区域信息 [数据持久化]
    /// </summary>
    [DBMapping("system_area")]
    public class AreaDAL : MongoOperation<AreaInfo>
    {
    }
}

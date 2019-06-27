using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 功能点信息 [数据持久化]
    /// </summary>
    [DBMapping("system_function")]
    public class FunctionDAL : MongoOperation<FunctionModuleInfo>
    {
    }
}

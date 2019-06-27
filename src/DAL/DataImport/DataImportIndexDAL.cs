using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 数据导入索引主表
    /// </summary>
    [DBMapping("import_index")]
    public class DataImportIndexDAL : MongoOperation<DataImportIndexInfo>
    {
    }
}

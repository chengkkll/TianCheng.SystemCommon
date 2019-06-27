using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 菜单信息 [数据持久化]
    /// </summary>
    [DBMapping("system_menu")]
    public class MenuMainDAL : MongoOperation<MenuMainInfo>
    {
    }
}

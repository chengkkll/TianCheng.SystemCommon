using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL;
using TianCheng.DAL.MongoDB;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.DAL
{
    /// <summary>
    /// 菜单信息 [数据持久化]
    /// </summary>
    [DBMapping("System_MenuInfo")]
    public class MenuMainDAL : MongoOperation<MenuMainInfo>
    {
    }
}

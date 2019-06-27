using TianCheng.BaseService;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemVersionService : MongoBusinessService<SystemVersion, TianCheng.Model.QueryInfo>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        public SystemVersionService(SystemVersionDAL dal)
            : base(dal)
        {
        }

        #endregion

        /// <summary>
        /// 更新数据库的结构支撑V2.0的版本
        /// </summary>
        /// <remarks>原1.0的数据结构与2.0的不同</remarks>
        public void UpdateToV2()
        {
            ((SystemVersionDAL)_Dal).UpdateToV2();
        }
    }
}

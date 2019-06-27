using TianCheng.BaseService;
using TianCheng.BaseService.Services;
using TianCheng.Model;
using TianCheng.SystemCommon.DAL;
using TianCheng.SystemCommon.Model;
using TianCheng.SystemCommon.Services;

namespace TianCheng.SystemCommon.Controller
{
    /// <summary>
    /// 数据导入控制器
    /// </summary>
    public class DataImportController<T, TD, TS, Info> : DataController
        where T : DataImportDetailInfo, new()
        where TD : DataImportDetailDAL<T>
        where TS : DataImportService<T, TD, Info>
        where Info : BusinessMongoModel, new()
    {
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        protected readonly FilePathService filePath;
        /// <summary>
        /// 
        /// </summary>
        protected readonly TS importService;
        /// <summary>
        /// 构造方法
        /// </summary>      
        public DataImportController()
        {
            filePath = ServiceLoader.GetService<FilePathService>();
            importService = ServiceLoader.GetService<TS>();
        }
        #endregion
    }
}

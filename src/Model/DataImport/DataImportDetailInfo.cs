using TianCheng.Excel;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 数据导入
    /// </summary>
    public class DataImportDetailInfo : TianCheng.Model.BusinessMongoModel
    {
        /// <summary>
        /// 序号      
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 导入的批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 导入状态
        /// </summary>
        public ImportState ImportState { get; set; }

        /// <summary>
        /// 导入失败原因
        /// </summary>
        [ExcelColumn("导入失败原因")]
        public string FailureReason { get; set; }
        /// <summary>
        /// 导入结果
        /// </summary>
        public string ImportResult { get; set; }
    }
}

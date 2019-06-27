namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 数据导入的结果
    /// </summary>
    public class DataImportDetailResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
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
        public string FailureReason { get; set; }
        /// <summary>
        /// 导入结果
        /// </summary>
        public string ImportResult { get; set; }
    }
}

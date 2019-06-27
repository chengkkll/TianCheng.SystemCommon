using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 数据导入索引对象
    /// </summary>
    public class DataImportIndexInfo : TianCheng.Model.BusinessMongoModel
    {
        /// <summary>
        /// 导入文件
        /// </summary>
        public FileNameView ImportFile { get; set; }
        /// <summary>
        /// 导入失败文件
        /// </summary>
        public FileNameView FailImportFile { get; set; }
        /// <summary>
        /// 导入的批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 导入数据总数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 已导入完成数量
        /// </summary>
        public int Current { get; set; }
        /// <summary>
        /// 是否导入完成
        /// </summary>
        public bool IsComplete { get; set; }
    }
}

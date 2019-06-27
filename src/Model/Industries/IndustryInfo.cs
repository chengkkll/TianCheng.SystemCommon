namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 行业信息
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class IndustryInfo : TianCheng.Model.BusinessMongoModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 行业说明
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 旧系统ID
        /// </summary>
        public int OldId { get; set; }
    }
}

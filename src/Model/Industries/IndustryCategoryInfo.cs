using System.Collections.Generic;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 行业分类信息
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class IndustryCategoryInfo : TianCheng.Model.BusinessMongoModel
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类中包含的行业信息
        /// </summary>
        public List<IndustryView> Industries { get; set; } = new List<IndustryView>();
    }
}

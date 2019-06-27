using AutoMapper;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class DataImportProfile : Profile, IAutoProfile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DataImportProfile()
        {
            Register();
        }

        /// <summary>
        /// 注册转换信息
        /// </summary>
        public void Register()
        {
            CreateMap<DataImportIndexInfo, DataImportIndexView>();
            CreateMap<DataImportIndexView, DataImportIndexInfo>();
        }
    }
}

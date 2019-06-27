using TianCheng.Model;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 部门的IP范围
    /// </summary>
    public class DepartmentIpInfo : BusinessMongoModel
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 部门可用的IP
        /// </summary>
        public string IpRange { get; set; }
    }
}

using Newtonsoft.Json;

namespace TianCheng.SystemCommon.Model
{
    /// <summary>
    /// 员工的查询条件
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class EmployeeQuery : TianCheng.Model.QueryInfo
    {
        /// <summary>
        /// 按名称模糊查询
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 查询关键字
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 按部门ID查询
        /// </summary>
        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 根节点部门
        /// </summary>
        public string RootDepartment { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [JsonProperty("roleId")]
        public string RoleId { get; set; }
        /// <summary>
        /// 按状态查询   1-可用，3-锁住，5-禁用
        /// </summary>
        [JsonProperty("state")]
        public UserState State { get; set; }

        /// <summary>
        /// 是否查看被逻辑删除的数据  0/不传值-不显示逻辑删除的数据 1-显示所有数据，包含逻辑删除的   2-只显示逻辑删除的数据
        /// </summary>
        [JsonProperty("hasDelete")]
        public int HasDelete { get; set; }
    }
}

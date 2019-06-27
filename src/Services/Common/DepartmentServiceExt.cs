using TianCheng.BaseService;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 部门服务扩展处理
    /// </summary>
    public class DepartmentServiceExt : IServiceExtOption
    {
        /// <summary>
        /// 设置扩展处理
        /// </summary>
        public void SetOption()
        {
            // 更新的后置处理
            DepartmentService.OnUpdated += EmployeeService.OnDepartmentUpdated;
        }
    }
}

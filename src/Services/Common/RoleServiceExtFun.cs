using TianCheng.BaseService;
using TianCheng.Model;

namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 角色操作的扩展方法
    /// </summary>
    static public class RoleServiceExtFun
    {
        /// <summary>
        /// 判断当前用户是否为管理员
        /// </summary>
        /// <param name="logonInfo"></param>
        /// <returns></returns>
        static public bool IsAdminRole(this TokenLogonInfo logonInfo)
        {
            RoleService roleService = ServiceLoader.GetService<RoleService>();
            if (roleService == null)
            {
                return false;
            }
            var roleInfo = roleService._SearchById(logonInfo.RoleId);
            if (roleInfo != null && roleInfo.Name.Contains("管理员"))
            {
                return true;
            }
            return false;
        }

    }
}

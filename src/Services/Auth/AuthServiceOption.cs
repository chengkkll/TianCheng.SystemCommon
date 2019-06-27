namespace TianCheng.SystemCommon.Services
{
    /// <summary>
    /// 登录验证配制
    /// </summary>
    public class AuthServiceOption
    {
        #region 设置静态访问方式
        /// <summary>
        /// 
        /// </summary>
        static private AuthServiceOption _Option = new AuthServiceOption();
        /// <summary>
        /// 
        /// </summary>
        static public AuthServiceOption Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        private AuthServiceOption()
        {

        }
        #endregion


        #region 扩展的配制信息
        private bool _IsLogonByTelephone = true;
        /// <summary>
        /// 是否可以用电话号码登录
        /// </summary>
        public bool IsLogonByTelephone { get { return _IsLogonByTelephone; } set { _IsLogonByTelephone = value; } }

        #endregion

        #region 扩展的事件处理

        #endregion
    }

}
